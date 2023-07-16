using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Room : BaseEntityModel
    {
        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public decimal Price { get; set; }
        public string Standard { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
