using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Dtos.OrderDtos
{
    public class OrderCreateDto
    {
        public List<OrderItemCreateDto> OrderItems { get; set; } = new List<OrderItemCreateDto>();
    }
}
