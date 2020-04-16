using Flunt.Validations;
using PaymenetContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string neighborhood, string city, string county, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            County = county;
            ZipCode = zipCode;

            AddNotifications(new Contract()
            .Requires()
            .HasMinLen(Street, 3, "Address.Street", "A rua deve conter pelo menos 3 caracteres.")
             ); 
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string County { get; private set; }
        public string ZipCode { get; private set; }
    }
}