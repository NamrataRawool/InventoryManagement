﻿using InventoryManagement.Common.Models;
using InventoryManagement.Common.Models.DTO;
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
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<TransactionDTO> Transactions { get; set; }
        public DbSet<CustomerDTO> Customers { get; set; }
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
            modelBuilder.Entity<ProductDTO>().ToTable("Products");
            modelBuilder.Entity<ProductDTO>(entity =>
            {
                entity.HasKey(e => e.ProductID);
                entity.Property(e => e.ProductID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CategoryDTO>().ToTable("Categories");
            modelBuilder.Entity<CategoryDTO>(entity =>
            {
                entity.HasKey(e => e.CategoryID);
                entity.Property(e => e.CategoryID).ValueGeneratedOnAdd();
            });
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Tax>().ToTable("Taxes");
            //modelBuilder.Entity<Tax>(entity =>
            //{
            //    entity.HasKey(e => e.TaxID);
            //    entity.Property(e => e.TaxID).ValueGeneratedOnAdd();
            //});
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionDTO>().ToTable("Transactions");
            modelBuilder.Entity<TransactionDTO>(entity =>
            {
                entity.HasKey(e => e.TransactionID);
                entity.Property(e => e.TransactionID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CustomerDTO>().ToTable("Customers");
            modelBuilder.Entity<CustomerDTO>(entity =>
            {
                entity.HasKey(e => e.CustomerID);
                entity.Property(e => e.CustomerID).ValueGeneratedOnAdd();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
