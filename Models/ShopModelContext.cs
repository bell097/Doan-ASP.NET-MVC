using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Doan_ASP.NET_MVC.Models
{
    public partial class ShopModelContext : DbContext
    {
        public ShopModelContext()
            : base("name=ShopModelContext13")
        {
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Image_detail> Image_detail { get; set; }
        public virtual DbSet<Origin> Origin { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<sale> sale { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Image_detail>()
                .Property(e => e.product_image_detail)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.product_image)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.product_size)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Image_detail)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}
