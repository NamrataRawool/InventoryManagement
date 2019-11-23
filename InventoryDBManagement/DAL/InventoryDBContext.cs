using InventoryManagement.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InventoryDBManagement.DAL
{
    public class InventoryDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        // public DbSet<ProductTransaction> ProductTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=InventoryDb.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductID);
                entity.Property(e => e.ProductID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryID);
                entity.Property(e => e.CategoryID).ValueGeneratedOnAdd();
            });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tax>().ToTable("Taxes");
            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasKey(e => e.TaxID);
                entity.Property(e => e.TaxID).ValueGeneratedOnAdd();
            });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>().ToTable("Transactions");
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionID);
                entity.Property(e => e.TransactionID).ValueGeneratedOnAdd();
            });
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<ProductTransaction>()
            //    .HasKey(pt => new { pt.ProductID, pt.TransactionID });
            //modelBuilder.Entity<ProductTransaction>()
            //    .HasOne<Product>(pt => pt.Product)
            //    .WithMany(p => p.ProductTransactions)
            //    .HasForeignKey(pt => pt.ProductID);

            //modelBuilder.Entity<ProductTransaction>()
            //    .HasOne<Transaction>(pt => pt.Transaction)
            //    .WithMany(t => t.ProductTransactions)
            //    .HasForeignKey(pt => pt.TransactionID);
        }
    }
}
