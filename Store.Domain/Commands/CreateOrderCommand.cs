using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : Notifiable<Notification>, ICommand
    {
        public CreateOrderCommand()
        {
            Items = new();
        }

        public CreateOrderCommand(
            string customerId,
            string zipCode,
            string promoCode,
            List<CreateOrderItemCommand> items)
        {
            CustomerId = customerId;
            ZipCode = zipCode;
            PromoCode = promoCode;
            Items = items;
        }

        public List<CreateOrderItemCommand> Items { get; set; }
        public string? CustomerId { get; set; }
        public string? ZipCode { get; set; }
        public string? PromoCode { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<CreateOrderCommand>()
                    .Requires()
                    .AreEquals(
                        CustomerId?.Length,
                        36,
                        "Customer ID",
                        "Cliente Inválido"
                    )
                    .AreEquals(
                        ZipCode?.Length,
                        8,
                        "ZipCode",
                        "CEP inválido"
                    )
            );
        }
    }
}