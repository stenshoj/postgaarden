using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden.Crud.Rooms;
using Postgaarden.Crud.Equipments;
using Postgaarden.Model.Rooms;
using System.Linq;
using Moq;
using Postgaarden.Connection;
using System.Collections.Generic;
using Postgaarden.Model.Equipments;

namespace PostgaardenUnitTest
{
    /*
        Developed by Chris Wohlert
    */
    [TestClass]
    public class SqliteRoomCrudUnitTest
    {
        Mock<DatabaseConnection> roomMock;
        Mock<EquipmentCrud> equipMock;
        RoomCrud crud;

        private RoomData data = new RoomData();

        public SqliteRoomCrudUnitTest()
        {
            roomMock = new Mock<DatabaseConnection>();
            equipMock = new Mock<EquipmentCrud>();
            crud = new SqliteRoomCrud(roomMock.Object, equipMock.Object);
        }
        /// <summary>
        /// Tests the create to SQL.
        /// </summary>
        [TestMethod]
        public void TestCreateToSql()
        {
            string sql = "";
            var room = new ConferenceRoom { Id = 1, Size = 10 };

            roomMock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Create(room);

            Assert.AreEqual("INSERT INTO ConferenceRoom (Id, Size) VALUES (1, 10);", sql);
        }

        /// <summary>
        /// Tests the read all.
        /// </summary>
        [TestMethod]
        public void TestReadAll()
        {
            var equipment = new List<Equipment> { new Equipment("Stol"), new Equipment("Tavle") };

            //ExecuteQuery returns an 2d array of objects, simulating a single entry in the table Room
            roomMock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => data.GetRoomAsObjectArray());

            //Read(Room room) returns IEnumerable<Equipment> simulating a call to SqliteEquipmentCrud
            equipMock.Setup(x => x.Read(It.IsAny<Room>())).Returns(() => equipment);

            var rooms = crud.Read().ToList();

            Assert.AreEqual(data.Rooms.First().Id, rooms.First().Id);
            Assert.AreEqual(data.Rooms.First().Name, rooms.First().Name);
            Assert.AreEqual(data.Rooms.First().Size, rooms.First().Size);
            CollectionAssert.AreEqual(equipment, rooms.First().Equipments);
        }

        /// <summary>
        /// Tests the read one.
        /// </summary>
        [TestMethod]
        public void TestReadOne()
        {
            var equipment = new List<Equipment> { new Equipment("Stol"), new Equipment("Tavle") };

            //ExecuteQuery returns a 2d array of objects, simulating a single entry in the table Room
            roomMock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => data.GetRoomAsObjectArray());

            //Read(Room room) returns IEnumerable<Equipment> simulating a call to SqliteEquipmentCrud
            equipMock.Setup(x => x.Read(It.IsAny<Room>())).Returns(() => equipment);

            var room = crud.Read(1);

            Assert.AreEqual(data.Rooms.First().Id, room.Id);
            Assert.AreEqual(data.Rooms.First().Name, room.Name);
            Assert.AreEqual(data.Rooms.First().Size, room.Size);
            CollectionAssert.AreEqual(equipment, room.Equipments);
        }

        /// <summary>
        /// Tests the update to SQL.
        /// </summary>
        [TestMethod]
        public void TestUpdateToSql()
        {
            string sql = "";
            var room = new ConferenceRoom { Id = 1, Size = 10 };

            roomMock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Update(room);

            Assert.AreEqual("UPDATE ConferenceRoom SET Size = 10 WHERE Id = 1;", sql);
        }

        /// <summary>
        /// Tests the delete to SQL.
        /// </summary>
        [TestMethod]
        public void TestDeleteToSql()
        {
            string sql = "";
            var room = new ConferenceRoom { Id = 1, Size = 10 };

            roomMock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Delete(room);

            Assert.AreEqual("DELETE FROM ConferenceRoom WHERE Id = 1;", sql);
        }
    }
}
