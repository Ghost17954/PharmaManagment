using Microsoft.Extensions.Logging;
using PharmaManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaManagment.Persistance
{
    public class PharmaContextSeed
    {
        public async static Task SeedOrder(PharmaContext context, ILogger<PharmaContextSeed> logger)
        {
            if (!context.Product.Any())
            {
                context.Product.AddRange(GetProductData());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database table Product associated with context {DbContextName}", typeof(PharmaContext).Name);
            }
            if (!context.Customer.Any())
            {
                context.Customer.AddRange(GetCustomerData());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database table Customer associated with context {DbContextName}", typeof(PharmaContext).Name);
            }
            if (!context.OrderDetail.Any())
            {
                context.OrderDetail.AddRange(GetOrderItenData(context));
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database table OrderItem associated with context {DbContextName}", typeof(PharmaContext).Name);
            }
        }

        private static IEnumerable<OrderDetails> GetOrderItenData(PharmaContext context)
        {
            return new List<OrderDetails>
            {
                new OrderDetails()
                {
                    CustomerId=1,
                    ProductId=1,
                    QuantityPurchased=2,
                    DateOfPurchase=DateTime.Now,
                    TotalPrice=1700,

                }
            };
        }

        private static IEnumerable<Customer> GetCustomerData()
        {
            return new List<Customer>
            {
                new Customer()
                {
                    Name ="Suresh",
                    EmailId ="Suresh@gmail.com",
                    Gender ="M",
                    Address ="Bengaluru",
                    UserPassword ="Suresh@123"
                },
                new Customer()
                {
                    Name="Ramesh",
                    EmailId="Ramesh@gmail.com",
                    Gender="M",
                    Address ="Mysore",
                    UserPassword="Ramesh@123"
                }
            };
        }

        private static IEnumerable<Products> GetProductData()
        {
            return new List<Products>
            {
                new Products()
                {
                    ProductName="Covaxin",
                    Manufacture=" Bharat Biotech",                    
                    Price=850,
                    QuantityAvailable=100,
                },
                new Products()
                {
                    ProductName ="Covishield",
                    Manufacture ="Serum Institute of India Pvt Ltd",
                    Price =780,
                    QuantityAvailable=100
                },
                new Products()
                {
                    ProductName ="AstraZeneca",
                    Manufacture="AstraZeneca",
                    Price=450,
                    QuantityAvailable=100,
                }
            };
        }
    }
}
