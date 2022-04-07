using System;

namespace PharmaManagment.Models.Dtos
{
    public class OrderDetailDto
    {
        public int OrderId { get; set; }
        public short QuantityPurchased { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double TotalPrice { get; set; }
        public CustomerDto Customer { get; set; }
        public ProductDto Product { get; set; }
    }
}
