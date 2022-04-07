using PharmaManagment.Models;
using PharmaManagment.Repository;
using System.Threading.Tasks;

namespace PharmaManagment.Services
{
    public  class CalculateTotalPrice
    {
         public readonly IPharmaRepository _repository;

        public CalculateTotalPrice(IPharmaRepository repository)
        {
            _repository = repository;
        }

        public  async Task<OrderDetails> GetTotalPrice(OrderDetails orderDetails)
        {
            var products=await _repository.GetProductsById(orderDetails.ProductId);
            orderDetails.TotalPrice = products.Price * orderDetails.QuantityPurchased;
            return orderDetails;
        }
    }
}
