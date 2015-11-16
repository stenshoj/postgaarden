using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden.Model.Rooms;
using System.Collections.Generic;
using Postgaarden.Model.Equipments;
using Postgaarden.Crud.Rooms;
using Moq;
using System.Linq;

namespace PostgaardenUnitTest
{
    /*
        Developed by Chris Wohlert
    */
    [TestClass]
    public class RoomHandlerUnitTest
    {
        private IEnumerable<Room> rooms = new RoomData().Rooms;
        /// <summary>
        /// Tests the filter by equipment.
        /// </summary>
        [TestMethod]
        public void TestFilterByEquipment()
        {
            var mock = new Mock<RoomCrud>();
            mock.Setup(x => x.Read()).Returns(rooms);
            var roomHandler = new RoomHandler(mock.Object);

            var filterList = new List<string> { "Stol", "Bord" };

            var result = roomHandler.Filter(filterList);

            //Returns only the rooms containing all the filter strings
            var testResult = rooms.Where(
                room => room.Equipments.Select(
                    equip => equip.Name).Intersect(filterList).Count() == filterList.Count).ToList();
            
            CollectionAssert.AreEqual(testResult.ToList(), result.ToList());
        }

        /// <summary>
        /// Tests the filter by size with minimum.
        /// </summary>
        [TestMethod]
        public void TestFilterBySizeWithMinimum()
        {
            var mock = new Mock<RoomCrud>();
            mock.Setup(x => x.Read()).Returns(rooms);
            var roomHandler = new RoomHandler(mock.Object);

            var result = roomHandler.Filter(8, true);

            var testResult = rooms.Where(room => room.Size >= 8);

            CollectionAssert.AreEqual(testResult.ToList(), result.ToList());
        }

        /// <summary>
        /// Tests the filter by size with out minimum.
        /// </summary>
        [TestMethod]
        public void TestFilterBySizeWithOutMinimum()
        {
            var mock = new Mock<RoomCrud>();
            mock.Setup(x => x.Read()).Returns(rooms);
            var roomHandler = new RoomHandler(mock.Object);

            var result = roomHandler.Filter(8, false);

            var testResult = rooms.Where(room => room.Size == 8);

            CollectionAssert.AreEqual(testResult.ToList(), result.ToList());
        }
    }
}
