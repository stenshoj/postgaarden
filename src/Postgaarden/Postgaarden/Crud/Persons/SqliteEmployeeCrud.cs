using Postgaarden.Connection;
using Postgaarden.Model.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgaarden.Crud.Persons
{
    public class SqliteEmployeeCrud : EmployeeCrud
    {
        public SqliteEmployeeCrud(DatabaseConnection connection)
        {
            this.DBConnection = connection;
        }
        //List<Employee> employeelist = new List<Employee>
        //{
        //    new Employee() { Id = 1,  Name = "Michael", EmailAddress = "Michael@mail.com"},
        //    new Employee() { Id = 2,  Name = "John", EmailAddress = "John@mail.com"},
        //    new Employee() { Id = 3,  Name = "Jordan", EmailAddress = "Jordan@mail.com"},
        //    new Employee() { Id = 4,  Name = "Sarah", EmailAddress = "Sarah@mail.com"},
        //    new Employee() { Id = 5,  Name = "Samantha", EmailAddress = "Samantha@mail.com"},
        //    new Employee() { Id = 6,  Name = "Ella", EmailAddress = "Ella@mail.com"}
        //};

        public override void Create(Employee entry)
        {
            DBConnection.ExecuteQuery("INSERT INTO Employee (Id, Name, EmailAddress) VALUES (1, Jens, EmailAddress)");
        }

        public override void Delete(Employee entry)
        {

        }

        public override IEnumerable<Employee> Read()
        {
            var rows = DBConnection.ExecuteQuery("SELECT Id, Name, EmailAddress FROM Employee");
            var employees = new List<Employee>();
            foreach (var row in rows)
            {
                Employee e = new Employee { Id = (int)row.ElementAt(0), Name = row.ElementAt(1).ToString(), EmailAddress = row.ElementAt(2).ToString() };
                employees.Add(e);
            }

            return employees;
        }

        public override Employee Read(Booking booking)
        {
            throw new NotImplementedException();
        }

        public override Employee Read(int key)
        {
            var employee = DBConnection.ExecuteQuery("SELECT Id, Name, EmailAddress FROM Employee WHERE Id="+key+"");
            return new Employee{ Id = (int)employee.First().ElementAt(0),
                                 Name = employee.First().ElementAt(1).ToString(),
                                 EmailAddress = employee.First().ElementAt(2).ToString()
                                 };
        }

        public override void Update(Employee entry)
        {
            
        }
    }
}