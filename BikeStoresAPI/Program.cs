using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Interfaces_Implementation;
using BikeStoresAPI.Models.DatabaseContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using AspNetCoreRateLimit;


var builder = WebApplication.CreateBuilder(args);

//MINE
builder.Services.AddDbContext<BikeStoresDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddScoped<Ibrands_repository, brands_repo_implementation>();
builder.Services.AddScoped<Icategories_repository, categories_repo_implementation>();
builder.Services.AddScoped<Icustomer_repository, customer_repo_implementation>();
builder.Services.AddScoped<Iorders_repository, orders_repo_implementation>();
builder.Services.AddScoped<Iproducts_repository, products_repo_implementation>();
builder.Services.AddScoped<Istaffs_repository, staffs_repo_implementation>();
builder.Services.AddScoped<Istocks_repository, stocks_repo_implementation>();
builder.Services.AddScoped<Istores_repository, stores_repo_implementation>();
builder.Services.AddScoped<Iuser_repository, users_repo_implementation>();
builder.Services.AddScoped<Ierror_handling, error_handling>();


//jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSetting:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod())
);
//rate limiting
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();
builder.Services.Configure<ClientRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "*",
                Period = "10s",
                Limit = 2
            }
        };
});




builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//MINE
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description ="Standard authorization header using the bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//MINE
app.UseAuthentication(); 
app.UseCors();
app.UseMiddleware<CustomClientRateLimitMiddleware>();



app.UseAuthorization();

app.MapControllers();

app.Run();
