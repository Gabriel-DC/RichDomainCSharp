using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Flunt.Validations;

namespace Store.Domain.Commands.Interfaces
{
    public class CreateOrderItemCommand : Notifiable<Notification>, ICommand
    {
        public CreateOrderItemCommand() { }

        public CreateOrderItemCommand(string productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public string? ProductId { get; set; }
        public int? Quantity { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<CreateOrderItemCommand>()
                .Requires()
                .AreEquals(
                    ProductId?.Length,
                    36,
                    "Product ID",
                    "Produto inválido")
                .IsGreaterThan(Quantity ?? 0, 0, "Quantity", "Quantidade inválida")
            );
        }
    }
}