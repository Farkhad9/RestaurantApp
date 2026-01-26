using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Models
{
    public class Order : BaseEntity
    {
        public string Number { get; set; } = null!;  
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; set; } 
        public DateTime Date { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
            Date = DateTime.Now;
        }

    }

}
