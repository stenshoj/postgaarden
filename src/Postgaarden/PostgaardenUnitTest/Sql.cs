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
        [TestMethod]
        public void TestCreateToSql()
        {
            string sql = "";
            var person = new Customer { CompanyName = "Merch", Name = "Jens", Cvr = "12345678", EmailAddress = "jens@mail.com" };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteCustomerCrud(mock.Object);

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

            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][] 
            {new object [] {"merch", "jens", "97865467", "thismail@mail.com" } });

            var customer = crud.Read("");

            Assert.AreEqual("merch", customer.CompanyName);
            Assert.AreEqual("jens", customer.Name);
            Assert.AreEqual("97865467", customer.Cvr);
            Assert.AreEqual("thismail@mail.com", customer.EmailAddress);
        }
    }
}
