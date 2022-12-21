using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class order_items
    {
        [Key]
        public int order_id { get; set; }
        public int item_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public decimal list_price { get; set; }
        public decimal discount { get; set; }
    }
}
