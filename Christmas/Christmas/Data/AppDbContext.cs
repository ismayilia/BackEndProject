using Christmas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Christmas.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Advert> Adverts { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Advert>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Review>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Customer>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Tag>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<BlogImage>().HasQueryFilter(m => !m.SoftDeleted);

        }
    }
}
