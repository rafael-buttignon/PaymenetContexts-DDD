using PaymenetContext.Domain.Repositories;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
        }

        public bool DocumentExists(string document)
        {
            if(document == "12345678901")
            return true;

            return false;
        }

        public bool EmailExists(string email)
        {
          if(email == "hello@balta.io")
            return true;

            return false;
        }
    }
}