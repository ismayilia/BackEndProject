﻿using Christmas.Models;
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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Setting> Settings { get; set; }
		public DbSet<ContactEmail> ContactEmails { get; set; }
		public DbSet<ContactInfo> ContactInfos { get; set; }
		public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<WishlistProduct> WishlistProducts { get; set; }

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
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<About>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Team>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Brand>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Setting>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<ContactInfo>().HasQueryFilter(m => !m.SoftDeleted);
			modelBuilder.Entity<ContactEmail>().HasQueryFilter(m => !m.SoftDeleted);
			modelBuilder.Entity<Subscribe>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Cart>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Wishlist>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<CartProduct>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<WishlistProduct>().HasQueryFilter(m => !m.SoftDeleted);
        }
    }
}
