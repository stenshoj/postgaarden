using Postgaarden.Connection;
using Postgaarden.Crud.Persons;
using Postgaarden.Crud.Rooms;
using Postgaarden.Model.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Text;
using System.Threading.Tasks;
using Postgaarden.Model.Rooms;
using System.Globalization;

namespace Postgaarden
{
    public class SqliteBookingCrud : BookingCrud
    {
        private DatabaseConnection @object;

        public RoomCrud RoomCrud
        {
            get;
            set;
        }

        public CustomerCrud CustomerCrud
        {
            get;
            set;
        }

        public EmployeeCrud EmployeeCrud
        {
            get;
            set;
        }

        public SqliteBookingCrud(DatabaseConnection connection, RoomCrud roomCrud, CustomerCrud customerCrud, EmployeeCrud employeeCrud)
        {
            this.DBConnection = connection;

            RoomCrud = roomCrud;
            CustomerCrud = customerCrud;
            EmployeeCrud = employeeCrud;
        }

        public override void Create(Booking entry)
        {
            var Id = DBConnection.ExecuteQuery($"INSERT INTO Booking (StartTime, EndTime, ConferenceRoomId, EmployeeId, CustomerCvr, Price) VALUES ({entry.StartTime.ToString("yyyy-mm-dd hh:mm")}, {entry.EndTime.ToString("yyyy-mm-dd hh:mm")}, {entry.Room.Id}, {((Employee)entry.Employee).Id}, {((Customer)entry.Customer).Cvr}, {entry.Price}); SELECT MAX (Id) FROM Booking;");
            entry.Id = Convert.ToInt32(Id.First().First());
        }

        public override void Delete(Booking entry)
        {
            var Id = DBConnection.ExecuteQuery($"DELETE FROM Booking WHERE Id={entry.Id};");
        }

        public override IEnumerable<Booking> Read()
        {
            var rows = DBConnection.ExecuteQuery("SELECT Id, StartTime, EndTime, RoomId, EmployeeId, CustomCvr, Price FROM Booking");
            var bookings = new List<Booking>();

            foreach (var row in rows)
            {
                Booking b = new Booking
                {
                    Id = Convert.ToInt32(row.ElementAt(0)),
                    Price = Convert.ToDouble(row.ElementAt(6))
                };
                b.Room = RoomCrud.Read(b);
                b.Employee = EmployeeCrud.Read(b);
                b.Customer = CustomerCrud.Read(b);
                DateTime StartTime = new DateTime();
                DateTime EndTime = new DateTime();
                DateTime.TryParseExact(row.ElementAt(1).ToString(), "yyyy-mm-dd,hh:mm", null, DateTimeStyles.None, out StartTime);
                DateTime.TryParseExact(row.ElementAt(2).ToString(), "yyyy-mm-dd,hh:mm", null, DateTimeStyles.None, out EndTime);
                b.SetTime(StartTime, EndTime);
                bookings.Add(b);
            }
            return bookings;
        }

        public override Booking Read(int key)
        {
            var booking = DBConnection.ExecuteQuery($"SELECT Id, StartTime, EndTime, ConferenceRoomId, EmployeeId, CustomCvr, Price FROM Booking WHERE Id={key};").First();

            Booking b = new Booking
            {
                Id = Convert.ToInt32(booking.ElementAt(0)),
                Price = Convert.ToDouble(booking.ElementAt(6))
            };

            b.Room = RoomCrud.Read(b);
            b.Employee = EmployeeCrud.Read(b);
            b.Customer = CustomerCrud.Read(b);
            DateTime StartTime = new DateTime();
            DateTime EndTime = new DateTime();
            DateTime.TryParseExact(booking.ElementAt(1).ToString(), "yyyy-mm-dd,hh:mm", null, DateTimeStyles.None, out StartTime);
            DateTime.TryParseExact(booking.ElementAt(2).ToString(), "yyyy-mm-dd,hh:mm", null, DateTimeStyles.None, out EndTime);
            b.SetTime(StartTime, EndTime);

            return b;
        }

        public override void Update(Booking entry)
        {
            var Id = DBConnection.ExecuteQuery($"UPDATE Booking SET StartTime={entry.StartTime}, EndTime={entry.EndTime}, ConferenceRoomId={entry.Room.Id}, Price={entry.Price} WHERE Id={entry.Id};");
        }
    }
}
