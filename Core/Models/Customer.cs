using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Booking : BaseEntityModel
    {
        public Booking(int roomId, int days = 1)
        {
            RoomId = roomId;
            Days = days;
        }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public int Days { get; set; } = 1;
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
