using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Order : BaseEntityModel
    {
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
