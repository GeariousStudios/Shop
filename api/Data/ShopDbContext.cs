using Microsoft.EntityFrameworkCore;
using Shop.api.Models;

namespace Shop.api.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
