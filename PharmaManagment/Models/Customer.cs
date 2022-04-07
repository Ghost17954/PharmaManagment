using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmaManagment.Models
{
    public class Customer
    {
        public Customer()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        [Key]
        public int CustomerId { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string UserPassword { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
