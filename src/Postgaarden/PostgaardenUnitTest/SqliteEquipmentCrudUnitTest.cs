using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Postgaarden.Connection;
using Postgaarden.Crud.Rooms;
using Postgaarden.Model.Equipments;
using Postgaarden.Model.Rooms;
using Postgaarden.Crud.Equipments;

namespace PostgaardenUnitTest
{
    /*
        Developed by Martin Hansen
    */

    [TestClass]
    public class SqliteEquipmentCrudUnitTest
    {
        /// <summary>
        /// Tests the create to SQL.
        /// </summary>
        [TestMethod]
        public void TestCreateToSql()
        {
            var sql = "";
            var equipment = new Equipment("Kaffemaskine");
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEquipmentCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s).Returns(() => new object[][] { new object[] { 1 } });

            crud.Create(equipment);

            Assert.AreEqual("INSERT INTO Equipment (Name) VALUES ('Kaffemaskine'); SELECT MAX(Id) FROM Equipment;", sql);
        }

        /// <summary>
        /// Tests the create return.
        /// </summary>
        [TestMethod]
        public void TestCreateReturn()
        {
            var equipment = new Equipment("Kaffemaskine");
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEquipmentCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][] { new object[] { 1 } });

            crud.Create(equipment);

            Assert.AreEqual(1, equipment.Id);
        }

        /// <summary>
        /// Tests the read.
        /// </summary>
        [TestMethod]
        public void TestReadOne()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEquipmentCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][] { new object[] { 1, "Kaffemaskine" } });

            var equipment = crud.Read(1);

            Assert.AreEqual(1, equipment.Id);
            Assert.AreEqual("Kaffemaskine", equipment.Name);
        }

        /// <summary>
        /// Tests the read one to SQL.
        /// </summary>
        [TestMethod]
        public void TestReadOneToSql()
        {
            var sql = "";
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEquipmentCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s).Returns(() => new object[][] { new object[] { 1, "Kaffemaskine" } });

            crud.Read(1);

            Assert.AreEqual("SELECT Id,Name FROM Equipment WHERE Id = 1;", sql);
        }

        /// <summary>
        /// Tests the read room.
        /// </summary>
        [TestMethod]
        public void TestReadRoom()
        {
            var data = new RoomData();
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEquipmentCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][] { new object[] { 1, "Kaffemaskiner" } });

            var equipments = crud.Read(data.Rooms.First());

            Assert.AreEqual(1, equipments.First().Id);
            Assert.AreEqual("Kaffemaskiner", equipments.First().Name);
        }

        /// <summary>
        /// Tests the read room to SQL.
        /// </summary>
        [TestMethod]
        public void TestReadRoomToSql()
        {
            var sql = "";
            var data = new RoomData();
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEquipmentCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s).Returns(() => new object[][] { new object[] { 1, "Kaffemaskiner" } });

            crud.Read(data.Rooms.First());

            Assert.AreEqual("SELECT Equipment.Id, Equipment.Name FROM Equipment JOIN Room ON Equipment.Id = Room.EquipmentId WHERE Room.Id = 1;", sql);
        }

        /// <summary>
        /// Tests the read all.
        /// </summary>
        [TestMethod]
        public void TestReadAll()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEquipmentCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new[] {new object [] {1, "Kaffemaskine" } });

            var equipments = crud.Read().ToList();

            Assert.AreEqual(1, equipments.First().Id);
            Assert.AreEqual("Kaffemaskine", equipments.First().Name);
        }

        /// <summary>
        /// Tests the read all to SQL.
        /// </summary>
        [TestMethod]
        public void TestReadAllToSql()
        {
            var sql = "";
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEquipmentCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s).Returns(() => new object[][] { new object[] { 1, "Kaffemaskine" } });

            crud.Read();

            Assert.AreEqual("SELECT Id,Name FROM Equipment;", sql);
        }

        /// <summary>
        /// Tests the delete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            var sql = "";
            var equipment = new Equipment("Kaffemaskine") { Id = 1 };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEquipmentCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Delete(equipment);

            Assert.AreEqual("DELETE FROM Equipment WHERE Id = 1;", sql);
        }

        /// <summary>
        /// Tests the update.
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            var sql = "";
            var equipment = new Equipment("F16 Fighting Falcon");
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEquipmentCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Update(equipment);

            Assert.AreEqual("UPDATE Equipment SET Name = 'F16 Fighting Falcon';", sql);
        }
    }
}
