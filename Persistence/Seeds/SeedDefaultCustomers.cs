using Core.Models;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public class SeedDefaultCustomers
    {
        private static readonly Random random = new Random();

        private static string[] firstNames = { "John", "Emma", "Michael", "Olivia", "William", "Ava", "James", "Sophia", "Benjamin", "Isabella" };
        private static string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Taylor", "Anderson" };

        public static void Seed(ApplicationDbContext context)
        {
            List<Customer> customers = new List<Customer>();

            // Generate 10 customers
            for (int i = 0; i < 10; i++)
            {
                string firstName = GenerateRandomFirstName();
                string lastName = GenerateRandomLastName();

                Customer customer = new Customer(firstName, lastName);
                customer.LoyaltyCard = GenerateRandomLoyaltyCard();

                // Add customer to the list
                customers.Add(customer);
            }

            // Save the customers to the database
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static string GenerateRandomFirstName()
        {
            // Generate a random index to get a random first name
            int index = random.Next(0, firstNames.Length);
            return firstNames[index];
        }

        private static string GenerateRandomLastName()
        {
            // Generate a random index to get a random last name
            int index = random.Next(0, lastNames.Length);
            return lastNames[index];
        }

        private static bool GenerateRandomLoyaltyCard()
        {
            // Generate a random boolean value for the LoyaltyCard property
            return random.Next(2) == 0; // 50% chance of true
        }
    }


}
