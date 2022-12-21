using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class customers
    {
        [Key]
        public int customer_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? zip_code { get; set; }
    }
}
