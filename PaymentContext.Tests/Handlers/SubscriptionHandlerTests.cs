using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.ValueObjects;
using Payment.Domain.Enums;
using PaymenetContext.Domain.Commands;
using PaymentContext.Tests.Mocks;
using PaymentContext.Domain.Handlers;
using System;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
         command.FirstName="Bruce";
         command.LastName="WAyne";
         command.Document="12345678901";
         command.Address="hello@balta.io2";
         command.BarCode="123456789";
         command.BoletoNumber="122424";
         command.PaymentNumber="2134124";
         command.PaidDate= DateTime.Now;
         command.ExpireDate= DateTime.Now.AddDays(30);
         command.Total=60;
         command.TotalPaid=60;
         command.Payer= "Wayne Corpo";
         command.PaymentNumber= "231231";
         command.PayerDocumentType=EDocumentType.CPF;
         command.PayerEmail="batman@dc.com";
         command.Street="asdder";
         command.Number="asdefe";
         command.Neighborhood="adadf";
         command.City="afefaef";
         command.County="efawe";
         command.ZipCode="241234234";
        
handler.Handler(command);
Assert.AreEqual(false, handler.Valid);
       } 
    }
}