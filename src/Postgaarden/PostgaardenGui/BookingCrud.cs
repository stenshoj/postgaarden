using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgaardenGui
{
    /// Made by Christoffer
    class BookingCrud
    {
        public IEnumerable<Booking> Read()
        {
            return new List<Booking>
            {
                new Booking(),
                new Booking(),
                new Booking(),
                new Booking(),
                new Booking()
            };
        }
        public void Create(Booking booking) { }

        public void Update(Booking booking) { }

        public void Delete(Booking booking) { }
    }
}
