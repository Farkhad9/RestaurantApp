using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Models
{
    public class OrderItem: BaseEntity
    {
        public string Number { get; set; } = null!;  
        public int MenuItemId { get; set; } 
        public MenuItem? MenuItem { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }  
        public int Count { get; set; }
    }
}
