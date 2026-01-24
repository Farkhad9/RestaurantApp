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

            builder.Property(mi => mi.Name)
                   .IsRequired()
                   .HasMaxLength(100);
                  
            builder.Property(mi => mi.Price)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(mi => mi.Category)
                   .IsRequired()
                   .HasMaxLength(50);



        }
    }
}
