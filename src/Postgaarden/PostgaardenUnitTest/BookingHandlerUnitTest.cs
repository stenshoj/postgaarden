using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden.Model.Bookings;
using Postgaarden;
using Moq;
using Postgaarden.Crud.Rooms;
using Postgaarden.Model.Persons;
using Postgaarden.Connection;
using Postgaarden.Model.Rooms;
using System.Linq;
using System.Collections.Generic;

namespace PostgaardenUnitTest
{
    [TestClass]
    public class BookingHandlerUnitTest
    {
        private Mock<BookingCrud> bookingMock;
        private Mock<RoomCrud> roomMock;
        private BookingHandler handler;
        private Data data;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingHandlerUnitTest"/> class.
        /// </summary>
        public BookingHandlerUnitTest()
        {
            data = new Data();
            bookingMock = new Mock<BookingCrud>();
            roomMock = new Mock<RoomCrud>();

            InitMock();

            handler = new BookingHandler(bookingMock.Object, new RoomHandler(roomMock.Object));
        }

        /// <summary>
        /// Initializes the mock.
        /// </summary>
        private void InitMock()
        {
            bookingMock.Setup(x => x.Read()).Returns(data.Bookings.ToList());
            roomMock.Setup(x => x.Read()).Returns(data.Rooms.ToList());
        }

        /// <summary>
        /// Tests the filter by date.
        /// </summary>
        [TestMethod]
        public void TestFilterByDate()
        {
            DateTime startTime = new DateTime(2015, 08, 02, 07, 00, 00);
            DateTime endTime = new DateTime(2015, 08, 02, 11, 00, 00);

            var result = handler.FilterByDate(startTime, endTime).ToList();

            var test = data.Bookings.ToList();
            test.RemoveAt(4);
            test.RemoveAt(3);
            test.RemoveAt(1);

            CollectionAssert.AreEqual(test, result);
        }

        /// <summary>
        /// Tests the get available rooms.
        /// </summary>
        [TestMethod]
        public void TestGetAvailableRooms()
        {
            DateTime startTime = new DateTime(2015, 08, 02, 07, 00, 00);
            DateTime endTime = new DateTime(2015, 08, 02, 11, 00, 00);
            int size = 4;
            var equipment = new List<string>();
            equipment.Add("Whiteboard");
            equipment.Add("Chair");

            var result = handler.GetAvailableRooms(startTime, endTime, size, equipment).ToList();

            var test = new List<Room>() { data.Rooms.ToList()[4] };

            CollectionAssert.AreEqual(test, result);
        }

        /// <summary>
        /// Tests the get available rooms as edit.
        /// </summary>
        [TestMethod]
        public void TestGetAvailableRoomsEdit()
        {
            Booking booking = data.Bookings.First(x => x.Id == 4);
            DateTime startTime = new DateTime(2015, 08, 03, 07, 00, 00);
            DateTime endTime = new DateTime(2015, 08, 03, 11, 00, 00);
            int size = 4;

            var result = handler.GetAvailableRooms(booking, startTime, endTime, size, new List<string>() { "Table" }).ToList();

            var test = new List<Room>() { data.Rooms.ToList()[0], data.Rooms.ToList()[1], data.Rooms.ToList()[3], data.Rooms.ToList()[5] };

            CollectionAssert.AreEqual(test, result);
        }
    }
}
