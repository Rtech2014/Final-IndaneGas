using System;
using System.Collections.Generic;
using System.Text;
using IndaneApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IndaneApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Stock>()
               .HasOne<Product>(a => a.Product)
               .WithMany(d => d.Stocks)
               .HasForeignKey(d => d.ProductId);


            //builder.Entity<Delivery>()
            //   .HasOne<Product>(a => a.Product)
            //   .WithMany(d => d.Deliveries)
            //   .HasForeignKey(d => d.ProductId);


            builder.Entity<Loading>()
               .HasOne<Product>(a => a.Product)
               .WithMany(d => d.Loadings)
               .HasForeignKey(d => d.ProductId);

            builder.Entity<OtherProductLoad>()
              .HasOne<Product>(a => a.Product)
              .WithMany(d => d.OtherProductLoads)
              .HasForeignKey(d => d.ProductId);

            builder.Entity<OtherProductSale>()
               .HasOne<Product>(a => a.Product)
               .WithMany(d => d.OtherProductSales)
               .HasForeignKey(d => d.ProductId);

            builder.Entity<OtherStock>()
               .HasOne<Product>(a => a.Product)
               .WithMany(d => d.OtherStocks)
               .HasForeignKey(d => d.ProductId);

            //builder.Entity<Delivery>()
            //  .HasOne<Full>(a => a.Fulls)
            //  .WithOne(d => d.Delivery)
            //  .HasForeignKey<OtherProductLoad>(d => d.OtherstockId);

            // builder.Entity<OtherProductSale>()
            //  .HasOne<OtherStock>(a => a.OtherStock)
            //  .WithOne(d => d.OtherProductSale)
            //  .HasForeignKey<OtherProductSale>(d => d.OtherstockId);

            // builder.Entity<Delivery>()
            // .HasOne<Stock>(a => a.Stock)
            // .WithOne(d => d.Delivery)
            // .HasForeignKey<Delivery>(d => d.StockId);

            // builder.Entity<Loading>()
            //.HasOne<Stock>(a => a.Stock)
            //.WithOne(d => d.Loading)
            //.HasForeignKey<Loading>(d => d.StockId);

            //builder.Entity<Delivery>()
            // .HasOne<DeliveryPersonDetail>(a => a.DeliveryPersonDetail)
            // .WithMany(d => d.Deliveries)
            // .HasForeignKey(d => d.PersonId);

            builder.Entity<Full>()
             .HasOne<DeliveryPersonDetail>(a => a.DeliveryPersonDetail)
             .WithMany(d => d.Fulls)
             .HasForeignKey(d => d.DeliveryPersonId);

            builder.Entity<Empty>()
             .HasOne<DeliveryPersonDetail>(a => a.DeliveryPersonDetail)
             .WithMany(d =>d.Empties)
             .HasForeignKey(d => d.DeliveryPersonId);

            builder.Entity<Empty>()
             .HasOne<Product>(a => a.Product)
             .WithMany(d => d.Empties)
             .HasForeignKey(d => d.ProductId);

            builder.Entity<Full>()
             .HasOne<Product>(a => a.Product)
             .WithMany(d => d.Fulls)
             .HasForeignKey(d => d.ProductId);


            builder.Entity<Empty>()
             .HasOne<Full>(a => a.Full)
             .WithOne(d => d.Empty)
             .HasForeignKey<Empty>(d => d.FullId); 
        }
        //public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Loading> Loadings { get; set; }
        public DbSet<OtherProductLoad> OtherProductLoads { get; set; }
        public DbSet<OtherProductSale> OtherProductSales { get; set; }
        public DbSet<OtherStock> OtherStocks { get; set; }
        public DbSet<DeliveryPersonDetail> DeliveryPersonDetails { get; set; }
        public DbSet<Full> Fulls { get; set; }
        public DbSet<Empty> Empties { get; set; }
    }
}
