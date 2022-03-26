using HPlusSportApi.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HPlusSportApi.Models
{

    public class ShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(c => c.Category)
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Seed();
        }
    }

}
