using System;

namespace PharmaManagment.Models.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Manufacture { get; set; }
        public double Price { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
