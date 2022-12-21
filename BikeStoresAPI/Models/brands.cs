using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class brands
    {
        [Key]
        public int brand_id { get; set; }
        [Required]
        [StringLength(255)]
        public string? brand_name { get; set; }
    }
}
