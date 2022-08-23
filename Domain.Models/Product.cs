﻿using System.Collections.Generic;

namespace Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal UnitPrice { get; set; }

        public ICollection<PortfolioProduct> PortfoliosProducts { get; set; } = new List<PortfolioProduct>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
