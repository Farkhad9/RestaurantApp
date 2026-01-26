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

            builder.Property(mi => mi.Number)
                .IsRequired()
                .HasMaxLength(50);

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

            // Seed данные
            builder.HasData(
                new MenuItem { Id = 1, Number = "M001", Name = "Piti", Price = 12.50m, Category = "Sup" },
                new MenuItem { Id = 2, Number = "M002", Name = "Dovga", Price = 8.00m, Category = "Sup" },
                new MenuItem { Id = 3, Number = "M003", Name = "Kebab", Price = 18.00m, Category = "Ana yemek" },
                new MenuItem { Id = 4, Number = "M004", Name = "Lyulya-kebab", Price = 15.00m, Category = "Ana yemek" },
                new MenuItem { Id = 5, Number = "M005", Name = "Plov", Price = 14.00m, Category = "Ana yemek" },
                new MenuItem { Id = 6, Number = "M006", Name = "Pakhlava", Price = 6.00m, Category = "Desert" },
                new MenuItem { Id = 7, Number = "M007", Name = "Shekerbura", Price = 5.50m, Category = "Desert" },
                new MenuItem { Id = 8, Number = "M008", Name = "Chay", Price = 2.00m, Category = "Icki" },
                new MenuItem { Id = 9, Number = "M009", Name = "Ayran", Price = 3.00m, Category = "Icki" },
                new MenuItem { Id = 10, Number = "M010", Name = "Sherbet", Price = 4.00m, Category = "Icki" }
            );
        }
    }

}
