using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgaarden
{
    public class SqliteBookingCrud : BookingCrud
    {
        public SqliteBookingCrud(DatabaseConnection connection)
        {
            this.DBConnection = connection;
        }

        public override void Create(Booking entry)
        {
            DBConnection.ExecuteQuery($"INSERT INTO Booking (StartTime, EndTime, RoomId, EmployeeId, CustomerCvr, Price) VALUES ({entry.StartTime.ToString("yyyy-mm-dd hh:mm")}, {entry.EndTime.ToString("yyyy-mm-dd hh:mm")}, {entry.Room.Id}, {((Employee)entry.Employee).Id}, {((Customer)entry.Customer).Cvr}, {entry.Price})");
        }

        public override void Delete(Booking entry)
        {
            
        }

        public override IEnumerable<Booking> Read()
        {
            var rows = DBConnection.ExecuteQuery("SELECT Id, StartTime, EndTime, RoomId, EmployeeId, CustomCvr, Price FROM Booking");
            var bookings = new List<Booking>();

            foreach (var row in rows)
            {
                Booking b = new Booking
                {
                    id = (int) row.ElementAt(0),
                    StartTime = (DateTime) row.ElementAt(1),
                    EndTime = (DateTime) row.ElementAt(2),
                    Room = RoomCrud.Read(row.ElementAt(3)),
                    Employee = EmployeeCrud.Read(row.ElementAt(4)),
                    Customer = CustomerCrud(row.ElementAt(5)),
                    Price = (int) row.ElementAt(6)
                };
        }
            return bookings;
        }

        public override Booking Read(int key)
        {
            return new Booking();
        }

        public override void Update(Booking entry)
        {
            
        }
    }
}
