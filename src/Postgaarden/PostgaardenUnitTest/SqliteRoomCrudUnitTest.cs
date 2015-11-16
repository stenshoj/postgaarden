using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden.Crud.Rooms;
using Postgaarden.Model.Rooms;
using System.Linq;
using Moq;
using Postgaarden.Connection;
using System.Collections.Generic;

namespace PostgaardenUnitTest
{
    /*
        Developed by Chris Wohlert
    */
    [TestClass]
    public class SqliteRoomCrudUnitTest
    {
        private RoomData data = new RoomData();
        /// <summary>
        /// Tests the create to SQL.
        /// </summary>
        [TestMethod]
        public void TestCreateToSql()
        {
            string sql = "";
            var room = new ConferenceRoom { Size = 10 };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteRoomCrud(mock.Object);
            
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Create(room);

            Assert.AreEqual("INSERT INTO ConferenceRoom (Size) VALUES (10)", sql);
        }

        /// <summary>
        /// Tests the read all.
        /// </summary>
        [TestMethod]
        public void TestReadAll()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteRoomCrud(mock.Object);

            //ExecuteQuery returns an 2d array of objects, simulating a single entry in the table Room
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => data.GetRoomAsObjectArray());

            var rooms = crud.Read().ToList();

            CollectionAssert.AreEqual(data.Rooms, rooms);
            Assert.AreEqual("Kaffemaskine", rooms.First().Equipments.First().Name);
        }

        /// <summary>
        /// Tests the read one.
        /// </summary>
        [TestMethod]
        public void TestReadOne()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteRoomCrud(mock.Object);

            //ExecuteQuery returns an 2d array of objects, simulating a single entry in the table Room
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => data.GetRoomAsObjectArray());
            
            var room = crud.Read(1);

            Assert.AreEqual(data.Rooms.First(), room);
        }

        /// <summary>
        /// Tests the update to SQL.
        /// </summary>
        [TestMethod]
        public void TestUpdateToSql()
        {
            string sql = "";
            var room = new ConferenceRoom { Id = 1, Size = 10 };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteRoomCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Update(room);

            Assert.AreEqual("UPDATE Room SET Size = 10 WHERE Id = 1", sql);
        }

        /// <summary>
        /// Tests the delete to SQL.
        /// </summary>
        [TestMethod]
        public void TestDeleteToSql()
        {
            string sql = "";
            var room = new ConferenceRoom { Id = 1, Size = 10 };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteRoomCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Update(room);

            Assert.AreEqual("DELETE FROM Room WHERE Id = 1", sql);
        }
    }
}
