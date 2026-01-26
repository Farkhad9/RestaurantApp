using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Dtos.OrderDtos
{
    public class OrderItemCreateDto
    {
        public int MenuItemId { get; set; }
        public int Count { get; set; }
    }
}
