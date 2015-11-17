using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Postgaarden;
using System.Linq;
using Postgaarden.Model.Persons;
using Postgaarden.Connection;
using Postgaarden.Crud.Persons;

namespace PostgaardenUnitTest
{
    [TestClass]
    public class Sql
    {
        //Created by Jens Kloster

        [TestMethod]
        public void TestCreateToSql()
        {
            string sql = "";
            var person = new Customer { CompanyName = "Merch", Name = "Jens", Cvr = "12345678", EmailAddress = "jens@mail.com" };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteCustomerCrud(mock.Object);

            //Execute query calls back the string sql containing the string from SqliteCustomerCrud
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Create(person);
            //Is used to force the SqliteCustomerCrud to use the Executequery (DBConnection.ExecuteQuery)
            //Can be used to verify other things when its used for employee
            mock.Verify(x => x.ExecuteQuery(It.IsAny<string>()));

            Assert.AreEqual("INSERT INTO Customer (CompanyName, Name, Cvr, EmailAddress) VALUES (Merch, Jens, 12345678, jens@mail.com)", sql);
        }

        [TestMethod]
        public void TestReadAll()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteCustomerCrud(mock.Object);

            //ExecuteQuery returns an 2d array of objects, simulating a single entry in the table Customer
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][]
            {new object [] {"merch", "jens", "97865467", "thismail@mail.com" } });

            var customers = crud.Read();

            Assert.AreEqual("merch", customers.First().CompanyName);
            Assert.AreEqual("jens", customers.First().Name);
            Assert.AreEqual("97865467", customers.First().Cvr);
            Assert.AreEqual("thismail@mail.com", customers.First().EmailAddress);
        }

        [TestMethod]
        public void TestReadOne()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteCustomerCrud(mock.Object);

            //ExecuteQuery returns an 2d array of objects, simulating a single entry in the table Customer
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][]
            {new object [] {"merch", "jens", "97865467", "thismail@mail.com" } });

            var customer = crud.Read("");

            Assert.AreEqual("merch", customer.CompanyName);
            //Assert.AreEqual
            Assert.AreEqual("jens", customer.Name);
            Assert.AreEqual("97865467", customer.Cvr);
            Assert.AreEqual("thismail@mail.com", customer.EmailAddress);
        }
        [TestMethod]
        public void TestUpdateCustomer()
        {
            string sql = "";
            var customer = new Customer { CompanyName = "merch", Name = "Sarah", Cvr = "12345678", EmailAddress = "Sarah@mail.com" };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteCustomerCrud(mock.Object);

            //Execute query calls back the string sql containing the string from SqliteCustomerCrud
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Update(customer);

            //Is used to force the SqliteCustomerCrud to use the Executequery (DBConnection.ExecuteQuery)
            mock.Verify(x => x.ExecuteQuery(It.IsAny<string>()));

            Assert.AreEqual($"UPDATE Customer SET CompanyName={customer.CompanyName}, Name={customer.Name}, EmailAddress={customer.EmailAddress} WHERE Cvr={customer.Cvr}",sql);
        }

        [TestMethod]
        public void TestDeleteCustomer()
        {
            string sql = "";
            var customer = new Customer { CompanyName = "merch", Name = "Sarah", Cvr = "12345678", EmailAddress = "Sarah@mail.com" };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteCustomerCrud(mock.Object);

            //Execute query calls back the string sql containing the string from SqliteCustomerCrud
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Delete(customer);

            //Is used to force the SqliteCustomerCrud to use the Executequery (DBConnection.ExecuteQuery)
            mock.Verify(x => x.ExecuteQuery(It.IsAny<string>()));

            Assert.AreEqual("DELETE FROM Customer WHERE Cvr=" + customer.Cvr + "", sql);
        }

        #region Developed By Chris
        [TestMethod]
        public void TestReadBookingToSql()
        {
            string stringToTest =
                "SELECT Cvr, Name, CompanyName, EmailAddress FROM Customer AS c " +
                "JOIN Booking AS b ON c.Cvr = b.CustumerCVR " + 
                "WHERE b.Id = 1;";
            string sql = "";

            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteCustomerCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Read(new Booking { Id = 1 });

            Assert.AreEqual(stringToTest, sql);
        }

        [TestMethod]
        public void TestReadBookingParse()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteCustomerCrud(mock.Object);

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>()))
                .Returns(new[] { new object[] { "12345678", "Lone Wolf", "Wolfgang", "Lone@Wolf.dk" } });

            var customer = crud.Read(new Booking());

            Assert.AreEqual("12345678", customer.Cvr);
            Assert.AreEqual("Long Wolf", customer.Name);
            Assert.AreEqual("Wolf inc", customer.CompanyName);
            Assert.AreEqual("Lone@Wolf.dk", customer.EmailAddress);
        }

        #endregion
    }
}
