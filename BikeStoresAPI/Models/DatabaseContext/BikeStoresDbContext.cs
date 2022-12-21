using Microsoft.EntityFrameworkCore;

namespace BikeStoresAPI.Models.DatabaseContext
{
    public class BikeStoresDbContext : DbContext
    {
        public BikeStoresDbContext(DbContextOptions<BikeStoresDbContext> options)
            : base(options)
        {

        }
        public DbSet<brands> brands { get; set; }
        public DbSet<categories> categories { get; set; }
        public DbSet<customers> customers { get; set; }
        public DbSet<order_items> order_items { get; set; }
        public DbSet<orders> orders { get; set; }
        public DbSet<products> products { get; set; }
        public DbSet<staffs> staffs { get; set; }
        public DbSet<stocks> stocks { get; set; }
        public DbSet<stores> stores { get; set; }
        public DbSet<users> users { get; set; }        
    }
}
