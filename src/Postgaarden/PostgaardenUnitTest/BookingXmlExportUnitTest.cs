using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden.Model.Persons;
using Postgaarden.Model.Rooms;
using Postgaarden.Model.Equipments;
using Moq;
using System.IO;
using Postgaarden;

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
            var writerMock = new Mock<TextWriter>();
            writerMock.Setup(x => x.Write(It.IsAny<string>())).Callback((string s) => xml = s);
            writerMock.Setup(x => x.WriteAsync(It.IsAny<string>())).Callback((string s) => xml = s);
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>())).Callback((string s) => xml = s);
            writerMock.Setup(x => x.WriteLineAsync(It.IsAny<string>())).Callback((string s) => xml = s);

            Assert.AreEqual(data, xml);
        }

        private string data =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<Booking xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                "<Id>1</Id>" +
                "<Price>399.0</Price>" +
                "<Startime>2000-08-08T00:00:00</Startime>" +
                "<EndTime>2000-08-09T00:00:00</EndTime>" +
                "<Employee>" +
                    "<Id>1</Id>" +
                    "<Name>Bo</Name>" +
                    "<EmailAddress>mail @mail.dk</EmailAddress>" +
                "</Employee>" +
                "<Customer>" +
                    "<CompanyName>Ecco</CompanyName>" +
                    "<CVR>12345678</CVR>" +
                    "<Name>Niels</Name>" +
                    "<EmailAddress>niels @mail.dk</EmailAddress>" +
                "</Customer>" +
                "<Room>" +
                    "<Id>1</Id>" +
                    "<Size>50</Size>" +
                    "<Name>Mødelokale 1</Name>" +
                    "<Equipments>" +
                        "<Equipment>" +
                            "<Id>1</Id>" +
                            "<Name>Stol</Name>" +
                        "</Equipment>" +
                        "<Equipment>" +
                            "<Id>2</Id>" +
                            "<Name>Grill</Name>" +
                        "</Equipment>" +
                    "</Equipments>" +
                "<Room>" +
            "</Booking>";
    }
}
