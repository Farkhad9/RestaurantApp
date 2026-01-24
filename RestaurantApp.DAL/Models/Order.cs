using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Models
{
    public class Order : BaseEntity
    {
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; private set; }
        public DateTime Date { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
            Date = DateTime.Now;
        }

    }

}
