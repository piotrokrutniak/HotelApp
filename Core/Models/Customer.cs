using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Customer : BaseEntityModel
    {
        public Customer(string firstName, string lastName) 
        { 
            FirstName = firstName;
            LastName = lastName;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool LoyaltyCard { get; set; } = false;
        public List<Booking> Bookings { get; set; }
    }
}
