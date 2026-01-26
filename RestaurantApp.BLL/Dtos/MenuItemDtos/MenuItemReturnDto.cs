using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Dtos.MenuItemDtos
{
    internal class MenuItemReturnDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
    }
}
