using Core.Models;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public static class SeedDefaultOrders
    {
        private static readonly Random random = new Random();

        public static void Seed(ApplicationDbContext context)
        {
            List<Customer> customers = context.Customers.ToList();
            List<Order> orders = new List<Order>();

            // Generate orders for customers
            foreach (Customer customer in customers)
            {
                Order order = new Order
                {
                    Total = 0.00m,
                    CustomerId = customer.Id,
                    Customer = customer
                };

                // Add order to the list
                orders.Add(order);
            }

            // Add additional orders if needed to reach a minimum count of 10
            while (orders.Count < 10)
            {
                int randomCustomerIndex = random.Next(customers.Count);
                Customer customer = customers[randomCustomerIndex];

                Order order = new Order
                {
                    Total = GenerateRandomTotal(),
                    CustomerId = customer.Id,
                    Customer = customer
                };

                // Add order to the list
                orders.Add(order);
            }

            // Save the orders to the database
            context.Orders.AddRange(orders);
            context.SaveChanges();
        }

        private static decimal GenerateRandomTotal()
        {
            // Generate a random total between 10 and 500
            return random.Next(1000, 50001) / 100.0m;
        }

    }
}
