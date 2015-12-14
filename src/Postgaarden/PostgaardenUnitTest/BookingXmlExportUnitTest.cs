using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden.Model.Persons;
using Postgaarden.Model.Rooms;
using Postgaarden.Model.Equipments;
using Moq;
using System.IO;
using Postgaarden;
using Postgaarden.Model.Bookings;
using System.Text;
using System.Xml.Serialization;

namespace PostgaardenUnitTest
{
    /*
        Developed by Christoffer Stenshøj
    */
    [TestClass]
    public class BookingXmlExportUnitTest
    {
        /// <summary>
        /// Tests the XML export.
        /// </summary>
        [TestMethod]
        public void TestXmlExport()
        {
            Type[] types = { typeof(Person), typeof(Customer), typeof(Employee), typeof(Room), typeof(ConferenceRoom), typeof(Equipment) };
            XmlSerializer serializer = new XmlSerializer(typeof(Booking), types);
            StringBuilder xml = new StringBuilder();
            Booking b = new Booking();
            Booking c;
            b.Id = 1;
            b.Price = 399;
            b.Employee = new Employee { EmailAddress = "mail@mail.dk", Id = 1, Name="Bo" };
            b.Customer = new Customer { CompanyName = "Ecco", Cvr = "12345678", Name = "Niels", EmailAddress = "niels@mail.dk" };
            b.SetTime(new DateTime(2000, 08, 08), new DateTime(2000, 08, 09));
            Room room = new ConferenceRoom();
            room.Equipments.Add(new Equipment("Stol") {Id=1});
            room.Equipments.Add(new Equipment("Grill") {Id=2});
            room.Id = 1;
            room.Size = 50;
            b.Room = room;
            
            StringWriter writer = new StringWriter(xml);
            BookingXmlExport.Export(b, writer);
            StringReader reader = new StringReader(xml.ToString());
            c = (Booking)serializer.Deserialize(reader);
            Assert.AreEqual(b.Id, c.Id);
            Assert.AreEqual(b.Price, c.Price);
            Assert.AreEqual(b.StartTime, c.StartTime);
            Assert.AreEqual(b.EndTime, c.EndTime);
            Assert.AreEqual(b.Employee.Name, c.Employee.Name);
            Assert.AreEqual(b.Employee.EmailAddress, c.Employee.EmailAddress);
            Assert.AreEqual(((Employee)b.Employee).Id, ((Employee)c.Employee).Id);
            Assert.AreEqual(b.Customer.Name, c.Customer.Name);
            Assert.AreEqual(b.Customer.EmailAddress, c.Customer.EmailAddress);
            Assert.AreEqual(((Customer)b.Customer).Cvr, ((Customer)c.Customer).Cvr);
            Assert.AreEqual(((Customer)b.Customer).CompanyName, ((Customer)c.Customer).CompanyName);
            Assert.AreEqual(b.Room.Id, c.Room.Id);
            Assert.AreEqual(b.Room.Size, c.Room.Size);
            Assert.AreEqual(b.Room.Name, c.Room.Name);
            Assert.AreEqual(b.Room.Equipments[0].Id, c.Room.Equipments[0].Id);
            Assert.AreEqual(b.Room.Equipments[0].Name, c.Room.Equipments[0].Name);
            Assert.AreEqual(b.Room.Equipments[1].Id, c.Room.Equipments[1].Id);
            Assert.AreEqual(b.Room.Equipments[1].Name, c.Room.Equipments[1].Name);

        }
    }
}
