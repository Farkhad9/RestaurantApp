using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Models
{
    public class MenuItem : BaseEntity
    {
       public string Number { get; set; } = null!;
       public string Name { get; set; } = null!;
       public decimal Price { get; set; }
       public string Category { get; set; } = null!;
    }
}
