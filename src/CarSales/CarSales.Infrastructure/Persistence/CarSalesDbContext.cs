using CarSales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Color = CarSales.Domain.Entities.Color;

namespace CarSales.Infrastructure.Persistence
{
    public class CarSalesDbContext : DbContext
    {
        public CarSalesDbContext(DbContextOptions<CarSalesDbContext> options)
            : base(options) { }

        public DbSet<Brand> Brands {  get; set; }
        public DbSet<Model> Models {  get; set; }
        public DbSet<Color> Colors {  get; set; }
        public DbSet<Trim> Trims {  get; set; }
        public DbSet<Order> Orders {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Model)
                .WithMany()
                .HasForeignKey(o => o.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Brand)
                .WithMany()
                .HasForeignKey(o => o.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Color)
                .WithMany()
                .HasForeignKey(o => o.ColorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Trim)
                .WithMany()
                .HasForeignKey(o => o.TrimId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
