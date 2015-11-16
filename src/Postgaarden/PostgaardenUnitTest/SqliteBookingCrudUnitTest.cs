using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden;
using Moq;
using System.Linq;

namespace PostgaardenUnitTest
{
    [TestClass]
    public class SqliteBookingCrudUnitTest
    {
        Booking booking = new Booking
        {
            EndTime = new DateTime(2015, 12, 11, 15, 00, 00),
            StartTime = new DateTime(2014, 12, 11, 15, 00, 00),
            Room = new Room { Name = "LeRoom", Size = 10, Id = 1, Equipments = new Equipment[] { new Equipment { Id = 1, Name = "CoffeeMACHINIMA" } } },
            Employee = new Employee { Name = "Jens", EmailAddress = "Jens@Jens.dk", Id = 1 },
            Customer = new Customer { Name = "Chris", EmailAddress = "Chris@Chris.dk", CompanyName = "Grundfos", Cvr = "19 34 27 35" },
            Price = 100
        };

        [TestMethod]
        public void TestCreateToSql()
        {
            string sql = "";
            
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteBookingCrud(mock.Object);

            //
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);
            crud.Create(booking);

            Assert.AreEqual($"INSERT INTO Booking (StartTime, EndTime, RoomId, EmployeeId, CustomerCvr, Price) VALUES ({booking.StartTime.ToString("yyyy-mm-dd hh:mm")}, {booking.EndTime.ToString("yyyy-mm-dd hh:mm")}, {booking.Room.Id}, {((Employee)booking.Employee).Id}, {((Customer)booking.Customer).Cvr}, {booking.Price})", sql);

        }

        [TestMethod]
        public void TestReadAll()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteBookingCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][] { new object[] { booking.StartTime.ToString("yyyy-mm-dd hh:mm"),
                                                                                                              booking.EndTime.ToString("yyyy-mm-dd hh:mm"),
                                                                                                              booking.Room.Id,
                                                                                                              ((Employee)booking.Employee).Id,
                                                                                                              ((Customer)booking.Customer).Cvr,
                                                                                                              booking.Price
                                                                                                            } });

            var bookings = crud.Read();

            var customer = bookings.First().Customer as Customer;
            var employee = bookings.First().Employee as Employee;
            var room = bookings.First().Room as Room;

            Assert.AreEqual(booking.StartTime.ToString("yyyy-mm-dd hh:mm"), bookings.First().StartTime);
            Assert.AreEqual(booking.EndTime.ToString("yyyy-mm-dd hh:mm"), bookings.First().EndTime);
            Assert.AreEqual(booking.Room, room);
            Assert.AreEqual(((Employee)booking.Employee).Id, employee.Id);
            Assert.AreEqual(((Customer)booking.Customer).Cvr, customer.Cvr);
            Assert.AreEqual(booking.Price, bookings.First().Price);
        }

        [TestMethod]
        public void TestReadOne()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteBookingCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][] { new object[] { booking.StartTime.ToString("yyyy-mm-dd hh:mm"),
                                                                                                              booking.EndTime.ToString("yyyy-mm-dd hh:mm"),
                                                                                                              booking.Room.Id,
                                                                                                              ((Employee)booking.Employee).Id,
                                                                                                              ((Customer)booking.Customer).Cvr,
                                                                                                              booking.Price
                                                                                                            } });

            var bookings = crud.Read(1);

            var customer = bookings.Customer as Customer;
            var employee = bookings.Employee as Employee;
            var room = bookings.Room as Room;

            Assert.AreEqual(booking.StartTime, bookings.StartTime);
            Assert.AreEqual(booking.EndTime, bookings.EndTime);
            Assert.AreEqual(booking.Room.Id, room.Id);
            Assert.AreEqual(((Employee)booking.Employee).Id, employee.Id);
            Assert.AreEqual(((Customer)booking.Customer).Cvr, customer.Cvr);
            Assert.AreEqual(booking.Price, bookings.Price);
        }
    }
}
