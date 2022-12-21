using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class stores
    {
        [Key]
        public int store_id { get; set; }
        [Required]
        [StringLength(255)]
        public string store_name { get; set; } = string.Empty;        
        [StringLength(25)]
        public string phone { get; set; } = string.Empty;        
        [StringLength(255)]
        public string email { get; set; } = string.Empty;        
        [StringLength(255)]
        public string street { get; set; } = string.Empty;        
        [StringLength(255)]
        public string city { get; set; } = string.Empty;        
        [StringLength(10)]
        public string state { get; set; } = string.Empty;        
        [StringLength(5)]
        public string zip_code { get; set; } = string.Empty;
    }
}
