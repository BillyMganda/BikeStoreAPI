using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Models
{
    public class users
    {
        [Key]
        public int id { get; set; }
        public string? email { get; set; }
        public string? password_hash { get; set; }
        public string? password_salt { get; set; }
        public DateTime date_created { get; set; }
    }
}
