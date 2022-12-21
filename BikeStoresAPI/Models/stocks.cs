using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class stocks
    {
        [Key]
        public int store_id { get; set; }
        [Required]
        public int product_id { get; set; }
        public int? quantity { get; set; }
    }
}
