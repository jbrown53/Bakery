using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bakery.Models
{
    public class Receipt
    {
        public string date { get; set; }
        public string item { get; set; }
        public int quantity { get; set; }
        public string employee { get; set; }
        public string customer { get; set; }
        public decimal price { get; set; }
        public decimal discount { get; set; }
        public decimal salesTax { get; set; }
        public decimal eatInTax { get; set; }
    }
}