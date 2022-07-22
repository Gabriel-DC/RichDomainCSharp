using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderItemCommand : ICommand
    {
        public CreateOrderItemCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public ValidationResult Validate() => new CreateOrderItemCommandValidator().Validate(this);

        public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
        {
            public CreateOrderItemCommandValidator()
            {
                RuleFor(c => c.ProductId)
                    .NotNull()
                    .NotEmpty()
                    .Configure(c => c.MessageBuilder = _ => $"Produto obrigatório");

                RuleFor(c => c.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantidade deve ser maior que zero");
            }
        }
    }
}