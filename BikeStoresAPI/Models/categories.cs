using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class categories
    {
        [Key]
        public int category_id { get; set; }
        [Required]
        [StringLength(255)]
        public string? category_name { get; set; }
    }
}
