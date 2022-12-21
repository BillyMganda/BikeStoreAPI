using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class staffs
    {
        [Key]
        public int staff_id { get; set; }
        [Required]
        public string first_name { get; set; } = string.Empty;
        [Required]
        public string last_name { get; set; } = string.Empty;
        [Required]
        public string email { get; set; } = string.Empty;        
        public string? phone { get; set; }
        [Required]
        public byte active { get; set; }
        [Required]
        public int store_id { get; set; }
        public int? manager_id { get; set; }
    }
}
