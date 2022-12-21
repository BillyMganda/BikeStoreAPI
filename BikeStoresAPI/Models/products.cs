using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class products
    {
        [Key]
        public int product_id { get; set; }
        public string product_name { get; set; } = string.Empty;
        public int brand_id { get; set; }
        public int category_id { get; set; }
        public Int16 model_year { get; set; }
        public decimal list_price { get; set; }
    }
}
