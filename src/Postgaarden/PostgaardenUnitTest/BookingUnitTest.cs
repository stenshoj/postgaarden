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
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StartTime_NotMoreThanEndTime_ThrowsArgOutOfRange()
        {
            Booking bookingStartTime = new Booking();
            DateTime datetime = new DateTime(2014, 11, 12, 12, 00, 00);
            bookingStartTime.StartTime = datetime;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EndTime_NotLessThanStartTime_ThrowsArgOutOfRange()
        {
            Booking bookingEndTime = new Booking();
            DateTime datetime = new DateTime(2015, 11, 12, 15, 00, 00);
            bookingEndTime.EndTime = datetime;
        }
    }
}
