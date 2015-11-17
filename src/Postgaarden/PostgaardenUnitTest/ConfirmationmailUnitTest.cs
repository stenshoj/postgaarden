using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postgaarden.Connection;
using PostgaardenMail;
using PostgaardenMail.Dummy;
//using Crud;

namespace PostgaardenUnitTest
{
    [TestClass]
    public class ConfirmationmailUnitTest
    {

        [TestMethod]
        public void TestMailText()
        {
            //var mail = new Mail { CompanyName = "Merch", Name = "Jessie", EmailAddress = "Jessie@mail.com", ConferenceRoom = "Tomoya", StartTime = "yyyy-mm-dd hh:mm", EndTime = "yyyy-mm-dd hh:mm" };
            //var emailText = $"The Conference room {mail.ConferenceRoom} has been booked to start {mail.StartTime} and end {mail.EndTime}, the name of the company registered to the booking is {mail.CompanyName}";
            ////var mock = new Mock<DatabaseConnection>();

            ////mock.Setup(x => x.ExecuteQuery(It.IsAny<string>())).Callback((string e) => emailtext = e);

            //Assert.AreEqual("Mail values from template", emailText);
        }
        [TestMethod]
        public void ConfirmEmailSend()
        {

        }
    }
}
