using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden;

namespace PostgaardenUnitTest
{
    [TestClass]
    public class BookingUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Price_NegativePrice_ThrowsArgOutOfRange()
        {
            Booking bookingPrice = new Booking();
            bookingPrice.Price = -1;
        }

        [TestMethod]
        public void TestSetTime_True()
        {
            DateTime startTime = new DateTime(2000, 1, 1, 12, 00, 00);
            DateTime endTime = new DateTime(2001, 1, 1, 15, 00, 00);

            Booking booking = new Booking();

            bool result = booking.SetTime(startTime, endTime);
            Assert.AreEqual(startTime, booking.StartTime);
            Assert.AreEqual(endTime, booking.EndTime);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSetTime_False()
        {
            DateTime startTime = new DateTime(2000, 1, 1, 12, 00, 00);
            DateTime endTime = new DateTime(200, 1, 1, 15, 00, 00);

            Booking booking = new Booking();

            bool result = booking.SetTime(startTime, endTime);
            Assert.AreNotEqual(startTime, booking.StartTime);
            Assert.AreNotEqual(endTime, booking.EndTime);

            Assert.IsFalse(result);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void StartTime_NotMoreThanEndTime_ThrowsArgOutOfRange()
        //{
        //    Booking bookingStartTime = new Booking();
        //    DateTime datetime = new DateTime(2014, 11, 12, 12, 00, 00);
        //    bookingStartTime.StartTime = datetime;
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void EndTime_NotLessThanStartTime_ThrowsArgOutOfRange()
        //{
        //    Booking bookingEndTime = new Booking();
        //    DateTime datetime = new DateTime(2015, 11, 12, 15, 00, 00);
        //    bookingEndTime.EndTime = datetime;
        //}
    }
}
