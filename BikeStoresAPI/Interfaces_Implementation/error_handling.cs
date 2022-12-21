using BikeStoresAPI.Interfaces;
using Newtonsoft.Json;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class error_handling : Ierror_handling
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public error_handling(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                if (context.Response.HasStarted)
                {
                    throw;
                }

                int statusCode = 500;
                //if (e is StorageUnavailableException || e is EmployerServiceUnavailableException)
                //{
                //    statusCode = 503;
                //}

                context.Response.Clear();
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    Message = e.Message,
                    Exception = SerializeException(e)
                };

                var body = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(body);
            }
        }
        public string? SerializeException(Exception e)
        {
            if (_webHostEnvironment.IsProduction())
            {
                // we do not include the Exception in production to avoid leaking sensitive information
                return null;
            }
            return e.ToString();
        }
    }
}
