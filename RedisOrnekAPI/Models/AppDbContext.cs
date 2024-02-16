using Microsoft.EntityFrameworkCore;

namespace RedisOrnekAPI.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Name = "omer1",
                    Id = 1,
                    Price = 50
                },
                new Product
                {
                    Name = "omer2",
                    Id = 2,
                    Price = 50
                },
                new Product
                {
                    Name = "omer3",
                    Id = 3,
                    Price = 50
                }); ;
            base.OnModelCreating(modelBuilder);
        }
    }
}
