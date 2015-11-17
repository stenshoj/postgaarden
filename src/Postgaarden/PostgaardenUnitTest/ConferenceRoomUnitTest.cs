using System;
using Postgaarden.Model.Rooms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PostgaardenUnitTest
{
    /*
        Developed by Chris Wohlert
    */
    [TestClass]
    public class ConferenceRoomUnitTest
    {
        /// <summary>
        /// Tests getting the name.
        /// </summary>
        [TestMethod]
        public void TestGetName()
        {
            ConferenceRoom room = new ConferenceRoom();
            room.Id = 1;
            Assert.AreEqual("Mødelokale 1", room.Name);
        }
    }
}
