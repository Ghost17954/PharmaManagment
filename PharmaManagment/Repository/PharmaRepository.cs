using Microsoft.EntityFrameworkCore;
using PharmaManagment.Models;
using PharmaManagment.Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaManagment.Repository
{
    public class PharmaRepository : IPharmaRepository
    {
        private readonly PharmaContext _context;
        public PharmaRepository(PharmaContext context)
        {
            _context = context;
        }
        public async Task<OrderDetails> CreateOrder(OrderDetails orderItems)
        {
            if (orderItems == null)
                return null;
           _context.Set<OrderDetails>().Add(orderItems);
            await _context.SaveChangesAsync();
            await UpdateQuantity(orderItems.QuantityPurchased,orderItems.ProductId);
            return orderItems;
        }

        public async Task DeleteOrder(int id)
        {
            var orderItem=await GetOrderById(id);
            if(orderItem != null)
                _context.Set<OrderDetails>().Remove(orderItem);
        }

        public async Task<IEnumerable<OrderDetails>> GetAllOrders()
        {
            return await _context.Set<OrderDetails>().ToListAsync();
        }

        public async Task<IEnumerable<Products>> GetAllProducts()
        {
            return await _context.Set<Products>().ToListAsync();
        }

        public async Task<OrderDetails> GetOrderById(int id)
        {
            return await _context.Set<OrderDetails>().FindAsync(id);
        }

        public async Task<Products> GetProductsById(int id)
        {
            return await _context.Set<Products>().FindAsync(id);
        }

        public async Task<bool> UpdateOrder(OrderDetails orderItems)
        {
            _context.Entry(orderItems).State = EntityState.Modified;
            int result=await _context.SaveChangesAsync();            
            return result>0?true:false;
        }
        
        public async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Set<Customer>().FindAsync(id);
        }

        public async Task UpdateQuantity(int quantityPurchased, int productId)
        {
            var product=await GetProductsById(productId);
            product.QuantityAvailable-=quantityPurchased;
            _context.Entry(product);
            var result=await _context.SaveChangesAsync();
        }
    }
}
