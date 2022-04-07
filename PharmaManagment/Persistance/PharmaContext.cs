using Microsoft.EntityFrameworkCore;
using PharmaManagment.Models;

namespace PharmaManagment.Persistance
{
    public class PharmaContext : DbContext
    {
        public PharmaContext(DbContextOptions<PharmaContext> options):base(options)
        {

        }

        public DbSet<Products> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<OrderDetails> OrderDetail { get; set; }
    }
}
