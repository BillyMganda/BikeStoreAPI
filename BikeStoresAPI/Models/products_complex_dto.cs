using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class products_complex_dto
    {        
        public int product_id { get; set; }
        public string product_name { get; set; } = string.Empty;       
        public Int16 model_year { get; set; }
        public decimal list_price { get; set; }
        public int brand_id { get; set; }
        public string? brand_name { get; set; }
        public int category_id { get; set; }
        public string? category_name { get; set; }
    }
}
