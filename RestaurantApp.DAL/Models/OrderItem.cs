using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Models
{
    public class OrderItem: BaseEntity
    {     
        public MenuItem MenuItem { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
    }
}
