using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Models
{
    public class OrderItem: BaseEntity
    {
        public int Count { get; set; }
    }
}
