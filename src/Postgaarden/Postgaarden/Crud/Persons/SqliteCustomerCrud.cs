﻿using Postgaarden.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postgaarden.Model.Persons;

namespace Postgaarden.Crud.Persons
{
    public class SqliteCustomerCrud : CustomerCrud
    {
        //Created by Jens Kloster

        public SqliteCustomerCrud(DatabaseConnection connection)
        {
            this.DBConnection = connection;
        }

        public override void Create(Customer entry)
        {
            //Create a new customer where the primary key is NOT Autoincrementet
            DBConnection.ExecuteQuery($"INSERT INTO Customer (CompanyName, Name, Cvr, EmailAddress) VALUES ({entry.CompanyName}, {entry.Name}, {entry.Cvr}, {entry.EmailAddress})");
        }

        public override void Delete(Customer entry)
        {
            //Deletes a customer based on the Cvr given when the method is called
            DBConnection.ExecuteQuery($"DELETE FROM Customer WHERE Cvr={entry.Cvr}");
        }

        public override IEnumerable<Customer> Read()
        {
            var rows = DBConnection.ExecuteQuery("SELECT CompanyName, Name, Cvr, EmailAddress FROM Customer");
            var customers = new List<Customer>();
            //Goes through a foreach loop to add all the customers to a list to be read
            foreach(var row in rows)
            {
                Customer c = new Customer { CompanyName = row.ElementAt(0).ToString(), Name = row.ElementAt(1).ToString(), Cvr = row.ElementAt(2).ToString(), EmailAddress = row.ElementAt(3).ToString() };
                customers.Add(c);
            }

            return customers;
        }

        public override Customer Read(Booking booking)
        {
            throw new NotImplementedException();
        }

        public override Customer Read(string key)
        {
            //Reads a single customer based on the Cvr given when the method is called
            var customer = DBConnection.ExecuteQuery("SELECT CompanyName, Name, Cvr, EmailAddress FROM Customer WHERE Cvr = "+key+"");
            return new Customer
            {
                CompanyName = customer.First().ElementAt(0).ToString(),
                Name = customer.First().ElementAt(1).ToString(),
                Cvr = customer.First().ElementAt(2).ToString(),
                EmailAddress = customer.First().ElementAt(3).ToString()
            };
        }
        
        public override void Update(Customer entry)
        {
            //Updates a customer based on the Cvr given when the method is called
            DBConnection.ExecuteQuery($"UPDATE Customer SET CompanyName={entry.CompanyName}, Name={entry.Name}, EmailAddress={entry.EmailAddress} WHERE Cvr={entry.Cvr}");
        }
    }
}