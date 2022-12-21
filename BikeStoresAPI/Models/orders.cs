using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class orders
    {
        [Key]
        public int order_id { get; set; }
        public int? customer_id { get; set; }
        public byte order_status { get; set; }
        public DateTime order_date { get; set; }
        public DateTime required_date { get; set; }
        public DateTime? shipped_date { get; set; }
        public int store_id { get; set; }
        public int staff_id { get; set; }
    }
}
