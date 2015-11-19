using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden;
using Moq;
using System.Linq;
using Postgaarden.Model.Rooms;
using Postgaarden.Model.Equipments;
using Postgaarden.Model.Persons;
using Postgaarden.Connection;
using Postgaarden.Crud.Rooms;
using Postgaarden.Crud.Persons;

namespace PostgaardenUnitTest
{
    [TestClass]
    public class SqliteBookingCrudUnitTest
    {
        Mock<DatabaseConnection> mock;
        BookingCrud crud;
        Mock<RoomCrud> roomCrud;
        Mock<CustomerCrud> customerCrud;
        Mock<EmployeeCrud> employeeCrud;

        Booking booking = new Booking
        {
            Id = 5,
            Room = new ConferenceRoom { Size = 10, Id = 1},
            Employee = new Employee { Name = "Jens", EmailAddress = "Jens@Jens.dk", Id = 1 },
            Customer = new Customer { Name = "Chris", EmailAddress = "Chris@Chris.dk", CompanyName = "Grundfos", Cvr = "19 34 27 35" },
            Price = 100
        };

        public SqliteBookingCrudUnitTest()
        {
            booking.SetTime(new DateTime(2000, 1, 1, 12, 00, 00), new DateTime(2001, 1, 1, 12, 00, 00));
            booking.Room.Equipments.Add(new Equipment("CoffeeMACHINIMA") { Id = 1 });

            mock = new Mock<DatabaseConnection>();
            roomCrud = new Mock<RoomCrud>();
            customerCrud = new Mock<CustomerCrud>();
            employeeCrud = new Mock<EmployeeCrud>();
            crud = new SqliteBookingCrud(mock.Object, roomCrud.Object, customerCrud.Object, employeeCrud.Object);
        }

        [TestMethod]
        public void TestCreateToSqlBooking()
        {
            string sql = "";

            mock = new Mock<DatabaseConnection>();
            roomCrud = new Mock<RoomCrud>();
            customerCrud = new Mock<CustomerCrud>();
            employeeCrud = new Mock<EmployeeCrud>();
            crud = new SqliteBookingCrud(mock.Object, roomCrud.Object, customerCrud.Object, employeeCrud.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s).Returns(() => new Object[][] { new[] { "1" } });
            crud.Create(booking);

            Assert.AreEqual($"INSERT INTO Booking (StartTime, EndTime, ConferenceRoomId, EmployeeId, CustomerCvr, Price) VALUES ({booking.StartTime.ToString("yyyy-MM-dd hh:mm")}, {booking.EndTime.ToString("yyyy-MM-dd hh:mm")}, {booking.Room.Id}, {((Employee)booking.Employee).Id}, {((Customer)booking.Customer).Cvr}, {booking.Price}); SELECT MAX (Id) FROM Booking;", sql);

        }

        [TestMethod]
        public void TestReadAllBooking()
        {
            mock = new Mock<DatabaseConnection>();
            roomCrud = new Mock<RoomCrud>();
            customerCrud = new Mock<CustomerCrud>();
            employeeCrud = new Mock<EmployeeCrud>();
            crud = new SqliteBookingCrud(mock.Object, roomCrud.Object, customerCrud.Object, employeeCrud.Object);

            roomCrud.Setup(x => x.Read(It.IsAny<Booking>())).Returns(new ConferenceRoom { Id = 1, Size = 4 });
            customerCrud.Setup(x => x.Read(It.IsAny<Booking>())).Returns(new Customer { Cvr = "12345678", Name = "Jens", CompanyName = "Bang og Olufsen", EmailAddress = "Bang@Olufsen.dk" });
            employeeCrud.Setup(x => x.Read(It.IsAny<Booking>())).Returns(new Employee { Id = 1, EmailAddress = "Something@Somewhere.dk", Name = "Anders" });

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][] { new object[] { booking.Id,
                                                                                                              booking.StartTime.ToString("yyyy-MM-dd,hh:mm"),
                                                                                                              booking.EndTime.ToString("yyyy-MM-dd,hh:mm"),
                                                                                                              booking.Room.Id,
                                                                                                              ((Employee)booking.Employee).Id,
                                                                                                              ((Customer)booking.Customer).Cvr,
                                                                                                              booking.Price
                                                                                                            } });

            var bookings = crud.Read();

            var customer = bookings.First().Customer as Customer;
            var employee = bookings.First().Employee as Employee;
            var room = bookings.First().Room as Room;

            Assert.AreEqual(booking.StartTime.ToString("yyyy-MM-dd hh:mm"), bookings.First().StartTime.ToString("yyyy-MM-dd hh:mm"));
            Assert.AreEqual(booking.EndTime.ToString("yyyy-MM-dd hh:mm"), bookings.First().EndTime.ToString("yyyy-MM-dd hh:mm"));
            Assert.AreEqual(1, room.Id);
            Assert.AreEqual(4, room.Size);
            Assert.AreEqual(1, employee.Id);
            Assert.AreEqual("Something@Somewhere.dk", employee.EmailAddress);
            Assert.AreEqual("Anders", employee.Name);
            Assert.AreEqual("12345678", customer.Cvr);
            Assert.AreEqual("Jens", customer.Name);
            Assert.AreEqual("Bang og Olufsen", customer.CompanyName);
            Assert.AreEqual("Bang@Olufsen.dk", customer.EmailAddress);
            Assert.AreEqual(booking.Price, bookings.First().Price);
        }

        [TestMethod]
        public void TestReadOneBooking()
        {
            mock = new Mock<DatabaseConnection>();
            roomCrud = new Mock<RoomCrud>();
            customerCrud = new Mock<CustomerCrud>();
            employeeCrud = new Mock<EmployeeCrud>();
            crud = new SqliteBookingCrud(mock.Object, roomCrud.Object, customerCrud.Object, employeeCrud.Object);

            roomCrud.Setup(x => x.Read(It.IsAny<Booking>())).Returns(new ConferenceRoom { Id = 1, Size = 4 });
            customerCrud.Setup(x => x.Read(It.IsAny<Booking>())).Returns(new Customer { Cvr = "12345678", Name = "Jens", CompanyName = "Bang og Olufsen", EmailAddress = "Bang@Olufsen.dk" });
            employeeCrud.Setup(x => x.Read(It.IsAny<Booking>())).Returns(new Employee { Id = 1, EmailAddress = "Something@Somewhere.dk", Name = "Anders" });

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][] { new object[] { booking.Id,
                                                                                                              booking.StartTime.ToString("yyyy-MM-dd,hh:mm"),
                                                                                                              booking.EndTime.ToString("yyyy-MM-dd,hh:mm"),
                                                                                                              booking.Room.Id,
                                                                                                              ((Employee)booking.Employee).Id,
                                                                                                              ((Customer)booking.Customer).Cvr,
                                                                                                              booking.Price
                                                                                                            } });

            var bookings = crud.Read(1);

            var customer = bookings.Customer as Customer;
            var employee = bookings.Employee as Employee;
            var room = bookings.Room as Room;

            Assert.AreEqual(booking.StartTime.ToString("yyyy-MM-dd hh:mm"), bookings.StartTime.ToString("yyyy-MM-dd hh:mm"));
            Assert.AreEqual(booking.EndTime.ToString("yyyy-MM-dd hh:mm"), bookings.EndTime.ToString("yyyy-MM-dd hh:mm"));
            Assert.AreEqual(1, room.Id);
            Assert.AreEqual(4, room.Size);
            Assert.AreEqual(1, employee.Id);
            Assert.AreEqual("Something@Somewhere.dk", employee.EmailAddress);
            Assert.AreEqual("Anders", employee.Name);
            Assert.AreEqual("12345678", customer.Cvr);
            Assert.AreEqual("Jens", customer.Name);
            Assert.AreEqual("Bang og Olufsen", customer.CompanyName);
            Assert.AreEqual("Bang@Olufsen.dk", customer.EmailAddress);
            Assert.AreEqual(booking.Price, bookings.Price);
        }

        [TestMethod]
        public void TestDeleteBooking()
        {
            string sql = "";

            mock = new Mock<DatabaseConnection>();
            roomCrud = new Mock<RoomCrud>();
            customerCrud = new Mock<CustomerCrud>();
            employeeCrud = new Mock<EmployeeCrud>();
            crud = new SqliteBookingCrud(mock.Object, roomCrud.Object, customerCrud.Object, employeeCrud.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s).Returns(() => new object[][] { new[] { "0" } });
            crud.Delete(booking);

            Assert.AreEqual($"DELETE FROM Booking WHERE Id={booking.Id};", sql);
        }

        [TestMethod]
        public void TestUpdateBooking()
        {
            string sql = "";

            mock = new Mock<DatabaseConnection>();
            roomCrud = new Mock<RoomCrud>();
            customerCrud = new Mock<CustomerCrud>();
            employeeCrud = new Mock<EmployeeCrud>();
            crud = new SqliteBookingCrud(mock.Object, roomCrud.Object, customerCrud.Object, employeeCrud.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s).Returns(() => new object[][] { new[] { "0" } });
            crud.Update(booking);

            Assert.AreEqual($"UPDATE Booking SET StartTime={booking.StartTime}, EndTime={booking.EndTime}, ConferenceRoomId={booking.Room.Id}, Price={booking.Price} WHERE Id={booking.Id};", sql);
        }
    }
}
