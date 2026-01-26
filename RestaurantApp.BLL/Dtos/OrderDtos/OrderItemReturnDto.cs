using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Dtos.OrderDtos
{
    public class OrderItemReturnDto
    {
        public int MenuItemId { get; set; }
        public string MenuItemNumber { get; set; } = null!;
        public string MenuItemName { get; set; } = null!;
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
