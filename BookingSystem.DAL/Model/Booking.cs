using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DAL.Model
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public Customer Customer { get; set; } = null!;
        public Room Room { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
