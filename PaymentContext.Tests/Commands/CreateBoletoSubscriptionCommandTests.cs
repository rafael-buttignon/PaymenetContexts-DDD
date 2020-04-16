using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.ValueObjects;
using Payment.Domain.Enums;
using PaymenetContext.Domain.Commands;

namespace PaymentContext.Tests
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        public void ShouldReturnNameIsInvalid()
        {
           var command = new CreateBoletoSubscriptionCommand();
           command.FirstName = "";

           command.Validate();
           Assert.AreEqual(false, command.Valid);
        }

        
    }
}