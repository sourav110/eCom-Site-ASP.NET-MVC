﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComShop.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderedAt { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
