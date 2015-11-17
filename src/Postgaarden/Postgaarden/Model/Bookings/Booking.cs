using Postgaarden.Model.Persons;
using Postgaarden.Model.Rooms;
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

        public int Id
        {
            get;
            set;
        }

        public DateTime StartTime
        {
            get;
            set;
        }

        public DateTime EndTime
        {
            get;
            set;
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

        public Booking()
        {
            StartTime = new DateTime();
            EndTime = new DateTime();
        }

        public Boolean SetTime(DateTime startTime, DateTime endTime)
        {
            if (startTime <= endTime)
            {
                this.StartTime = startTime;
                this.EndTime = endTime;

                return true;
            }
            return false;
        }
    }
}