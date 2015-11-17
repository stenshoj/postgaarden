using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden.Model.Persons;
using Postgaarden.Model.Rooms;
using Postgaarden.Model.Equipments;
using Moq;
using System.IO;

namespace PostgaardenUnitTest
{
    [TestClass]
    public class BookingXmlExportUnitTest
    {
        [TestMethod]
        public void TestXmlExport()
        {
            string xml = "";
            Booking b = new Booking();
            b.Employee = new Employee { EmailAddress = "mail@mail.dk", Id = 1, Name="Bo" };
            b.Customer = new Customer { CompanyName = "Ecco", Cvr = "12345678", Name = "Niels", EmailAddress = "niels@mail.dk" };
            b.SetTime(new DateTime(2000, 08, 08), new DateTime(2000, 08, 09));
            Room room = new ConferenceRoom();
            room.Equipments.Add(new Equipment("Stol"));
            room.Equipments.Add(new Equipment("Grill"));
            b.Room = room;
            var writerMock = new Mock<TextWriter>();
            writerMock.Setup(x => x.Write(It.IsAny<string>())).Callback((string s) => xml = s);
            writerMock.Setup(x => x.WriteAsync(It.IsAny<string>())).Callback((string s) => xml = s);
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>())).Callback((string s) => xml = s);
            writerMock.Setup(x => x.WriteLineAsync(It.IsAny<string>())).Callback((string s) => xml = s);

            Assert.AreEqual(data, xml);
        }

        private string data =
            "";
    }
}
