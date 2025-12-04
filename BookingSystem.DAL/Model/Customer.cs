using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DAL.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
