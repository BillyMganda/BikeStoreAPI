using AspNetCoreRateLimit;
using BikeStoresAPI.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class CustomClientRateLimitMiddleware : ClientRateLimitMiddleware
    {
        private readonly IConfiguration _configuration;
        public CustomClientRateLimitMiddleware(RequestDelegate next, IProcessingStrategy processingStrategy,
            IOptions<ClientRateLimitOptions> options, IClientPolicyStore policyStore,
            IRateLimitConfiguration config, ILogger<ClientRateLimitMiddleware> logger, IConfiguration configuration)
            : base(next, processingStrategy, options, policyStore, config, logger)
        {
            _configuration = configuration;
        }

        public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            string? path = httpContext?.Request?.Path.Value;
            var result = JsonSerializer.Serialize("API calls quota exceeded!");
            httpContext.Response.Headers["Retry-After"] = retryAfter;
            httpContext.Response.StatusCode = 429;
            httpContext.Response.ContentType = "application/json";

            WriteQuotaExceededResponseMetadata(path, retryAfter);
            return httpContext.Response.WriteAsync(result);
        }

        private async void WriteQuotaExceededResponseMetadata(string requestPath, string retryAfter)
        {
            //Code to write data to the database
            string connection_string = _configuration.GetSection("ConnectionStrings:DBConnection").Value;
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO rate_limiter(request_path,retry_after,status_code,date_created) VALUES(@request_path,@retry_after,@status_code,@date_created)";
                    command.Connection = connection;
                    command.Parameters.AddWithValue("request_path", requestPath);
                    command.Parameters.AddWithValue("retry_after", retryAfter);
                    command.Parameters.AddWithValue("status_code", 429);
                    command.Parameters.AddWithValue("date_created", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
    
}
