using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden;
using PostgaardenMail;
using Postgaarden.Model.Persons;
using Postgaarden.Model.Rooms;
using System.Collections.Generic;

namespace PostgaardenUnitTest
{
    [TestClass]
    public class EmailConfirmationUnitTest
    {
        //Developed by Jens Kloster
        [TestMethod]
        public void TestEmailReciever()
        {
            Postgaarden.Mailtemplate mail = new Postgaarden.Mailtemplate();
            Postgaarden.Booking booking = new Postgaarden.Booking();

            booking.Room.Id = 1;
            DateTime startTime = new DateTime(2015, 1, 1, 15, 00, 00);
            booking.StartTime = new DateTime(2015, 1, 1, 15, 00, 00);
            DateTime endTime = new DateTime(2015, 1, 1, 16, 00, 00);
            booking.EndTime = new DateTime(2015, 1, 1, 16, 00, 00);
            //var bookingString = "";
            List<string> equipmentlist = new List<string> { "CoffeeMaker", "TV", "Projecter" };
            List<string> trylist = new List<string> { "CoffeeMaker", "TV", "Projecter" };
            ((Customer)booking.Customer).Name = "Jessie";
            ((Customer)booking.Customer).EmailAddress = "Jessie@gmail.com";
            ((Customer)booking.Customer).Cvr = "65432178";
            ((Customer)booking.Customer).CompanyName = "Merch";
            ((Employee)booking.Employee).Name = "James";

            //foreach (var e in equipmentlist)
            //{
            //    bookingString += e + "\n";
            //}

            mail.Receiver = "rmail@gmail.com";
            mail.Sender = "smail@gmail.com";
            mail.Subject = "Faktura lige nu";
            mail.Body = "<h1>Hotel Postgaarden booking</h1>";
            mail.Body += @"<p> Tak for at have booked et rum ved Hotel Postgaarden <br /> 
            Denne mail er til for at bekræfte din ordre </p >";
            mail.Body += "<p>Conferance rum " + booking.Room.Id + @" <br />
            Din booking af rummet starter " + booking.StartTime + ", og slutter " + booking.EndTime + @" <br />
            Størrelsen på rummet du har bestilt og udstyr i rummet " + equipmentlist + @"
            Faktureringsinformation: <br />
            " + ((Customer)booking.Customer).Name + @"<br />
            " + ((Customer)booking.Customer).EmailAddress + @" <br />
            " + ((Customer)booking.Customer).Cvr + @" <br />
            " + ((Customer)booking.Customer).CompanyName + @" <br />
            Total pris på booking: <br />
            " + 1 + @" < br /> </p>";
            mail.Signature = "<h2>Best Regards</h2>";
            mail.Signature += "<p>" + ((Employee)booking.Employee).Name + @" <br /> 
            Hotel Postgaarden<p>";
            mail.UserName = "postgaardentest@gmail.com";
            mail.Password = "posttest";

            Assert.AreEqual(booking.Room.Id, 1);
            //Assert.AreEqual(booking.StartTime, startTime);
            //Assert.AreEqual(booking.EndTime, endTime);
            //Assert.AreEqual(equipmentlist, trylist);
            //Assert.AreEqual(((Customer)booking.Customer).Name, "Jessie");
            //Assert.AreEqual(((Customer)booking.Customer).EmailAddress, "Jessie@gmail.com");
            //Assert.AreEqual(((Customer)booking.Customer).Cvr, "65432178");
            //Assert.AreEqual(((Customer)booking.Customer).CompanyName, "Merch");
            //Assert.AreEqual(((Employee)booking.Employee).Name, "James");
        }

        [TestMethod]
        public void TestEmailSubject()
        {
            //Postgaarden.Mailtemplate tempmail = new Postgaarden.Mailtemplate
            //{
            //    Receiver = "rmail@gmail.com",
            //    Sender = "smail@gmail.com",
            //    Subject = "Faktura lige NU",
            //    Body = "<h1>booking fra hotellet</h1>",
            //    Signature = "<h2>Best Regards</h2>",
            //    UserName = "postgaardentest@gmail.com",
            //    Password = "posttest"
            //};

            //Assert.AreEqual(tempmail.Receiver, "rmail@gmail.com");
            
            
        }

        [TestMethod]
        public void TestEmailBodyContent()
        {
            //Postgaarden.Mailtemplate mailtemp = new Postgaarden.Mailtemplate();
            //mailtemp.Receiver = "rmail@gmail.com";
            //mailtemp.Sender = "thismail@gmail.com";

            //This works for what reason ??
            //Assert.AreEqual(mailtemp.Receiver, "rmail@gmail.com");
            //Assert.AreEqual(mailtemp.Sender, "thismail@gmail.com");
        }
    }
}
