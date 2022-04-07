using System;

namespace PharmaManagment.Models.Dtos
{
    public class OrderDetailsCreateDto
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public short QuantityPurchased { get; set; }
        public DateTime DateOfPurchase { get; set; }    
    }
}
