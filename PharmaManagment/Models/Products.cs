using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmaManagment.Models
{
    public class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Manufacture { get; set; }
        public double Price { get; set; }
        public int QuantityAvailable { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
