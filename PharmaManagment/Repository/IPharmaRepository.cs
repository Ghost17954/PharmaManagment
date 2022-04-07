using PharmaManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaManagment.Repository
{
    public interface IPharmaRepository
    {
        Task<IEnumerable<Products>> GetAllProducts();
        Task<Products> GetProductsById(int id);
        Task<OrderDetails> CreateOrder(OrderDetails orderItems);
        Task<bool> UpdateOrder(OrderDetails orderItems);
        Task DeleteOrder(int id);
        Task<IEnumerable<OrderDetails>> GetAllOrders();
        Task<OrderDetails> GetOrderById(int id);
        Task<Customer> GetCustomerById(int id);
        Task UpdateQuantity(int quantityPurchased,int productId);
    }
}
