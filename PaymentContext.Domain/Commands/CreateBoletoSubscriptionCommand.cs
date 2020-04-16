using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymenetContext.Shared.Commands;
using Payment.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymenetContext.Domain.Commands
{
    public class CreateBoletoSubscriptionCommand : Notifiable ,ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Address { get; set; }
        public string BarCode { get; set; }
        public string BoletoNumber { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public Document PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
         public string PayerEmail { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres.")
                .HasMinLen(LastName, 3, "Name.LastName", "Nome deve conter pelo menos 3 caracteres.")
                .HasMaxLen(FirstName, 40, "Name.FirstName", "Nome deve conter até 40 caracteres.")
                .HasMaxLen(LastName, 40, "Name.LastName", "Nome deve conter até 40 caracteres.")
            );    
        }
    }
}