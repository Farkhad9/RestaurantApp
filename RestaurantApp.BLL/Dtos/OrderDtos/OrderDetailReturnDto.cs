using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Dtos.OrderDtos
{
    public class OrderDetailReturnDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public int MenuItemCount { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItemReturnDto> OrderItems { get; set; } = new List<OrderItemReturnDto>();
    }
}
