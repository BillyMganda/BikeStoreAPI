namespace BikeStoresAPI.Interfaces
{
    public interface Ierror_handling
    {
        public Task Invoke(HttpContext context);
        public string? SerializeException(Exception e);
    }
}
