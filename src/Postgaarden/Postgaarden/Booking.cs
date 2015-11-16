using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgaarden
{
    public class Booking
    {
        private double price;
        private DateTime starttime;
        private DateTime endtime;

        public int id
        {
            get;
            set;
        }

        public DateTime StartTime
        {
            get
            {
                return starttime;
            }
            set
            {
                if (starttime > endtime)
                {
                    throw new ArgumentOutOfRangeException("StartTime can not be later than the EndTime");
                }
                starttime = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return endtime;
            }
            set
            {
                if(endtime < starttime)
                {
                    throw new ArgumentOutOfRangeException("StartTime can not be later than the EndTime");
                }
                endtime = value;
            }
        }

        public Room Room
        {
            get;
            set;
        }

        public Person Employee
        {
            get;
            set;
        }

        public Person Customer
        {
            get;
            set;
        }

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Price must not be under 0");
                }
                price = value;
            }
        }
    }
}