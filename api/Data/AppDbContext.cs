using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.api.Models;

namespace Shop.api.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserProduct>(u => u.HasKey(p => new { p.AppUserId, p.ProductId }));
            builder.Entity<UserImage>(u => u.HasKey(i => new { i.AppUserId, i.ImageId }));
            builder.Entity<UserReview>(u => u.HasKey(r => new { r.AppUserId, r.ReviewId }));

            builder
                .Entity<UserProduct>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.UserProducts)
                .HasForeignKey(p => p.AppUserId);

            builder
                .Entity<UserProduct>()
                .HasOne(u => u.Product)
                .WithMany(u => u.UserProducts)
                .HasForeignKey(p => p.ProductId);

            builder
                .Entity<UserImage>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.UserImages)
                .HasForeignKey(i => i.AppUserId);

            builder
                .Entity<UserImage>()
                .HasOne(u => u.Image)
                .WithMany(u => u.UserImages)
                .HasForeignKey(i => i.ImageId);

            builder
                .Entity<UserReview>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.UserReviews)
                .HasForeignKey(i => i.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<UserReview>()
                .HasOne(u => u.Review)
                .WithMany(u => u.UserReviews)
                .HasForeignKey(i => i.ReviewId)
                .OnDelete(DeleteBehavior.NoAction);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER",
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
