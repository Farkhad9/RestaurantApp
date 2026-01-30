using Microsoft.EntityFrameworkCore;
using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestaurantApp.DAL.Data
{
    public class RestaurantAppDbContext : DbContext
    {
        public DbSet<MenuItem> MenuItems { get; set; }  
        public DbSet<Order>Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public RestaurantAppDbContext(DbContextOptions<RestaurantAppDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RestaurantAppDb;Trusted_Connection=True;TrustServerCertificate=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantAppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
    }
}
