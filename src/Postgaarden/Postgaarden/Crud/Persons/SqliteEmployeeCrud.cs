﻿using Postgaarden.Connection;
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
        //Created by Jens Kloster

        public SqliteEmployeeCrud(DatabaseConnection connection)
        {
            this.DBConnection = connection;
        }

        public override void Create(Employee entry)
        {
            //Creates a new employee with an Autoincrementet employee id
            DBConnection.ExecuteQuery($"INSERT INTO Employee (Id, Name, EmailAddress) VALUES ({entry.Id}, {entry.Name}, {entry.EmailAddress})");
        }

        public override void Delete(Employee entry)
        {
            //Deletes a single employee based on the id given when the method is called
            DBConnection.ExecuteQuery($"DELETE FROM Employee WHERE Id="+entry.Id+"");
        }

        public override IEnumerable<Employee> Read()
        {
            var rows = DBConnection.ExecuteQuery("SELECT Id, Name, EmailAddress FROM Employee");
            var employees = new List<Employee>();
            //Goes through a foreach loop to add all the employees to the 'employees' list to be read
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
            //Returns a single employee based on the id given when the method is called 
            var employee = DBConnection.ExecuteQuery("SELECT Id, Name, EmailAddress FROM Employee WHERE Id="+key+"");
            return new Employee{ Id = (int)employee.First().ElementAt(0),
                                 Name = employee.First().ElementAt(1).ToString(),
                                 EmailAddress = employee.First().ElementAt(2).ToString()
                                 };
        }
        
        public override void Update(Employee entry)
        {
            //Updates an employee based on the id given when the method is called
            DBConnection.ExecuteQuery($"UPDATE Employee SET Name={entry.Name}, EmailAddress={entry.EmailAddress} WHERE Id={entry.Id}");
        }
    }
}