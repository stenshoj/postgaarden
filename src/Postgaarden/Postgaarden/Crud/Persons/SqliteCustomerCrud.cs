using Postgaarden.Connection;
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
        public SqliteCustomerCrud(DatabaseConnection connection)
        {
            this.DBConnection = connection;
        }

        //List<Customer> customerlist = new List<Customer>
        //{
        //    new Customer() {Cvr = "12345678", CompanyName = "Merch", Name = "Chuck", EmailAddress = "Chuck@hotmail.com"},
        //    new Customer() {Cvr = "98126572", CompanyName = "GoatCO", Name = "Billy", EmailAddress = "Billy@hotmail.com"},
        //    new Customer() {Cvr = "67545398", CompanyName = "FamersMarket", Name = "Martha", EmailAddress = "Martha@hotmail.com"},
        //    new Customer() {Cvr = "53217890", CompanyName = "DrummingWorld", Name = "Chad", EmailAddress = "Chad@hotmail.com"}
        //};

        public override void Create(Customer entry)
        {
            DBConnection.ExecuteQuery("INSERT INTO Customer (CompanyName, Name, Cvr, EmailAddress) VALUES (Merch, Jens, 12345678, jens@mail.com)");
        }

        public override void Delete(Customer entry)
        {

        }

        public override IEnumerable<Customer> Read()
        {
            var rows = DBConnection.ExecuteQuery("SELECT CompanyName, Name, Cvr, EmailAddress FROM Customer");
            var customers = new List<Customer>();
            foreach(var row in rows)
            {
                Customer c = new Customer { CompanyName = row.ElementAt(0).ToString(), Name = row.ElementAt(1).ToString(), Cvr = row.ElementAt(2).ToString(), EmailAddress = row.ElementAt(3).ToString() };
                customers.Add(c);
            }

            return customers;

            //return new Customer[] { new Customer { CompanyName = rows.ElementAt(0).First().ToString() },
            //                        new Customer { Name = rows.ElementAt(1).First().ToString() },
            //                        new Customer { Cvr = rows.ElementAt(2).First().ToString() },
            //                        new Customer { EmailAddress = rows.ElementAt(3).First().ToString() }};
        }

        public override Customer Read(string key)
        {
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

        }
    }
}