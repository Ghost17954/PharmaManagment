using System;
using System.ComponentModel.DataAnnotations;

namespace PharmaManagment.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public short QuantityPurchased { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double TotalPrice { get; set; }
        public Customer Customer { get; set; }
        public Products Product { get; set; }
    }
}
