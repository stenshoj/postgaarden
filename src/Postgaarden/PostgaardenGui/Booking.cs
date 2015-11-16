using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgaardenGui
{
    public class Booking
    {   
        public int Id { get; set; }     
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Price{get; set;}
        public int ConferenceRoomId { get; set; }
        public string CustomerCVR { get; set; }
        public int EmployeeId { get; set; }
        public string Pricestr { get{return Price.ToString("0.00" + " " + "DKK"); } }

        public Booking()
        {
            Id = 1;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now.AddHours(3);
            Price = 499;
            ConferenceRoomId = 1;
            CustomerCVR = "1234568";
            EmployeeId = 52947;
            
        }
    }
}
