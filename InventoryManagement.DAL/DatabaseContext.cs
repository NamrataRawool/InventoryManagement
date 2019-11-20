using InventoryManagement.Common.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace InventoryManagement.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connStringBuilder = new SqliteConnectionStringBuilder { DataSource = "InventoryDb.db" };
            //var connectionString = connStringBuilder.ToString();
            //var connection = new SqliteConnection(connectionString);

            //optionsBuilder.UseSqlite(connection);

            optionsBuilder.UseSqlite("Filename=InventoryDb.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Product>().ToTable("Products", "test");
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
