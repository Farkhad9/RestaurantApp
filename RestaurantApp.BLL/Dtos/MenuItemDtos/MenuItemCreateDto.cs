using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Dtos.MenuItemDtos
{
    public class MenuItemCreateDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
    }
}
