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
    public class EmployeeUnitTest
    {
        //Created by Jens Kloster

        [TestMethod]
        public void TestCreateToSqlEmployee()
        {
            string sql = "";
            var person = new Employee { Id = 1, Name = "Jessie", EmailAddress = "Jessie@mail.com" };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEmployeeCrud(mock.Object);

            //Execute query calls back the string sql containing the string from SqliteEmployeeCrud
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Create(person);
            //Is used to force the SqliteCustomerCrud to use the Executequery (DBConnection.ExecuteQuery)
            //Can be used to verify other things when its used for employee
            mock.Verify(x => x.ExecuteQuery(It.IsAny<string>()));

            //Checks the string that is send whether or not its the same as the one written below
            Assert.AreEqual($"INSERT INTO Employee (Id, Name, EmailAddress) VALUES ({person.Id}, {person.Name}, {person.EmailAddress})", sql);
        }
        [TestMethod]
        public void TestReadAllEmployee()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEmployeeCrud(mock.Object);

            //ExecuteQuery returns an 2d array of objects, simulating a single entry in the table Employee
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][]
            {new object [] {1, "Jessie", "Jessie@mail.com"} });

            var employees = crud.Read();

            Assert.AreEqual(1, employees.First().Id);
            Assert.AreEqual("Jessie", employees.First().Name);
            Assert.AreEqual("Jessie@mail.com", employees.First().EmailAddress);
        }
        [TestMethod]
        public void TestReadOneEmployee()
        {
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEmployeeCrud(mock.Object);

            //ExecuteQuery returns an 2d array of objects, simulating a single entry in the table Employee
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Returns(() => new object[][]
            {new object [] {1,"Jessie", "Jessie@mail.com"}});

            var employee = crud.Read(1);

            Assert.AreEqual(1, employee.Id);
            Assert.AreEqual("Jessie", employee.Name);
            Assert.AreEqual("Jessie@mail.com", employee.EmailAddress);
        }
        [TestMethod]
        public void TestUpdateEmployee()
        {
            string sql = "";
            var employee = new Employee { Id = 1, Name = "Jessie", EmailAddress = "Jessie@mail.com" };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEmployeeCrud(mock.Object);

            //Execute query calls back the string sql containing the string from SqliteEmployeeCrud
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Update(employee);

            //Is used to force the SqliteCustomerCrud to use the Executequery (DBConnection.ExecuteQuery)
            mock.Verify(x => x.ExecuteQuery(It.IsAny<string>()));

            //Checks the string that is send whether or not its the same as the one written below
            Assert.AreEqual($"UPDATE Employee SET Name={employee.Name}, EmailAddress={employee.EmailAddress} WHERE Id={employee.Id}", sql);
        }
        [TestMethod]
        public void TestDeleteEmployee()
        {
            string sql = "";
            var employee = new Employee {Id = 1, Name = "Jessie", EmailAddress = "Jessie@mail.com" };
            var mock = new Mock<DatabaseConnection>();
            var crud = new SqliteEmployeeCrud(mock.Object);

            //This is used to callback the string whith the information that you send (the Sql statement)
            mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string s) => sql = s);

            crud.Delete(employee);

            //Is used to force the SqliteCustomerCrud to use the Executequery (DBConnection.ExecuteQuery)
            mock.Verify(x => x.ExecuteQuery(It.IsAny<string>()));

            //Checks the string that is send whether or not its the same as the one written below
            Assert.AreEqual("DELETE FROM Employee WHERE Id="+employee.Id+"", sql);
            
        }
    }
}
