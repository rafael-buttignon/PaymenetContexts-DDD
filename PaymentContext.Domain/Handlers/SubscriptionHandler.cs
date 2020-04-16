using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymenetContext.Domain.Commands;
using PaymenetContext.Domain.Repositories;
using PaymenetContext.Shared.Commands;
using Payment.Domain.Enums;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable,
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePaypalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailServices _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailServices emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();

            if(command.Invalid){
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura!");
            }
            AddNotifications(new Contract());
            //Verificar se Documento já esta cadastrado

            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso");
            }
            //Verificar se E-mail já esta cadastro
            if(_repository.EmailExists(command.Document))
            {
                AddNotification("Email", "Este Email já está em uso");
            }
            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Address);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.County, command.ZipCode);
            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument.ToString(), command.PayerDocumentType), 
                address, email);

             //Relacionamentos   
             subscription.AddPayment(payment);
             student.AddSubscription(subscription);
            //Agrupar as validaçoes

            AddNotifications(name, document, email, address, student, subscription, payment);
            //Salvar as informaçoes

            _repository.CreateSubscription(student);
            //Enviar email de boas vindas

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");
            //Retornar informaçoes

            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handler(CreatePaypalSubscriptionCommand command)
        {
           
            AddNotifications(new Contract());
            //Verificar se Documento já esta cadastrado

            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso");
            }
            //Verificar se E-mail já esta cadastro
            if(_repository.EmailExists(command.Document))
            {
                AddNotification("Email", "Este Email já está em uso");
            }
            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Address);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.County, command.ZipCode);
            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument.ToString(), command.PayerDocumentType), 
                address, email);

             //Relacionamentos   
             subscription.AddPayment(payment);
             student.AddSubscription(subscription);
            //Agrupar as validaçoes

            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as notificaçoes
            if(Invalid)
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");

            //Salvar as informaçoes

            _repository.CreateSubscription(student);
            //Enviar email de boas vindas

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");
            //Retornar informaçoes

            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}