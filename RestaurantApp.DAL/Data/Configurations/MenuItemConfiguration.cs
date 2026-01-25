using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Data.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(mi => mi.Id);
            
            builder.ToTable("MenuItems");
            builder.Property(x=>x.Number)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(mi => mi.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(mi => mi.Name)
            .IsUnique();

            builder.Property(mi => mi.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(mi => mi.Category)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasData(
                new MenuItem { Id = 1, Number = "001", Name = "Margherita Pizza", Price = 8.99m, Category = "Main Course" },
                new MenuItem { Id = 2, Number = "002", Name = "Caesar Salad", Price = 5.49m, Category = "Appetizer" },
                new MenuItem { Id = 3, Number = "003", Name = "Chocolate Lava Cake", Price = 6.99m, Category = "Dessert" },
                new MenuItem { Id = 4, Number = "004", Name = "Spaghetti Carbonara", Price = 10.99m, Category = "Main Course" },
                new MenuItem { Id = 5, Number = "005", Name = "Bruschetta", Price = 4.99m, Category = "Appetizer" },
                new MenuItem { Id = 6, Number = "006", Name = "Tiramisu", Price = 5.99m, Category = "Dessert" },
                new MenuItem { Id = 7, Number = "007", Name = "Grilled Salmon", Price = 12.99m, Category = "Main Course" }
            ); 


        }
    }
}
