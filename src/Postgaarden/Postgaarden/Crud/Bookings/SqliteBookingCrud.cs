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

        /// <summary>
        /// Gets or sets the room crud.
        /// </summary>
        /// <value>
        /// The room crud.
        /// </value>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteBookingCrud"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="roomCrud">The room crud.</param>
        /// <param name="customerCrud">The customer crud.</param>
        /// <param name="employeeCrud">The employee crud.</param>
        public SqliteBookingCrud(DatabaseConnection connection, RoomCrud roomCrud, CustomerCrud customerCrud, EmployeeCrud employeeCrud)
        {
            this.DBConnection = connection;

            RoomCrud = roomCrud;
            CustomerCrud = customerCrud;
            EmployeeCrud = employeeCrud;
        }

        /// <summary>
        /// Creates the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public override void Create(Booking entry)
        {
            var Id = DBConnection.ExecuteQuery($"INSERT INTO Booking (StartTime, EndTime, ConferenceRoomId, EmployeeId, CustomerCVR, Price) VALUES ('{entry.StartTime.ToString("yyyy-MM-dd hh:mm")}', '{entry.EndTime.ToString("yyyy-MM-dd hh:mm")}', {entry.Room.Id}, {((Employee)entry.Employee).Id}, '{((Customer)entry.Customer).Cvr}', {entry.Price}); SELECT MAX (Id) FROM Booking;");
            entry.Id = Convert.ToInt32(Id.First().First());
        }

        public override void Delete(Booking entry)
        {
            var Id = DBConnection.ExecuteQuery($"DELETE FROM Booking WHERE Id={entry.Id};");
        }

        public override IEnumerable<Booking> Read()
        {
            var rows = DBConnection.ExecuteQuery("SELECT Id, StartTime, EndTime, ConferenceRoomId, EmployeeId, CustomerCVR, Price FROM Booking;");
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
                DateTime StartTime = Convert.ToDateTime(row.ElementAt(1).ToString());
                DateTime EndTime = Convert.ToDateTime(row.ElementAt(2).ToString());
                b.SetTime(StartTime, EndTime);
                bookings.Add(b);
            }
            return bookings;
        }

        public override Booking Read(int key)
        {
            var booking = DBConnection.ExecuteQuery($"SELECT Id, StartTime, EndTime, ConferenceRoomId, EmployeeId, CustomerCVR, Price FROM Booking WHERE Id={key};").First();

            Booking b = new Booking
            {
                Id = Convert.ToInt32(booking.ElementAt(0)),
                Price = Convert.ToDouble(booking.ElementAt(6))
            };

            b.Room = RoomCrud.Read(b);
            b.Employee = EmployeeCrud.Read(b);
            b.Customer = CustomerCrud.Read(b);
            b.SetTime(Convert.ToDateTime(booking.ElementAt(1).ToString()), Convert.ToDateTime(booking.ElementAt(2).ToString()));
            return b;
        }

        public override void Update(Booking entry)
        {
            var Id = DBConnection.ExecuteQuery($"UPDATE Booking SET StartTime='{entry.StartTime.ToString("yyyy-MM-dd hh:MM")}', EndTime='{entry.EndTime.ToString("yyyy-MM-dd hh:mm")}', ConferenceRoomId={entry.Room.Id}, Price={entry.Price}, EmployeeId = {((Employee)entry.Employee).Id}, CustomerCVR = {((Customer)entry.Customer).Cvr} WHERE Id={entry.Id};");
        }
    }
}
