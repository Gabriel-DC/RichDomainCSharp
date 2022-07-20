using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Store.Domain.Commands.Interfaces;
using static Store.Domain.Commands.CreateOrderItemCommand;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : ICommand
    {
        public CreateOrderCommand(Guid customerId, string zipCode, string promoCode, IEnumerable<CreateOrderItemCommand> items)
        {
            CustomerId = customerId;
            ZipCode = zipCode;
            PromoCode = promoCode;
            Items = items;
        }

        public Guid CustomerId { get; set; }
        public string ZipCode { get; set; }
        public string PromoCode { get; set; }
        public IEnumerable<CreateOrderItemCommand> Items { get; set; }

        public ValidationResult Validate()
            => new CreateOrderCommandValidator().Validate(this);


        public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
        {
            public CreateOrderCommandValidator()
            {
                RuleFor(c => c.CustomerId)
                    .NotEmpty()
                    .WithMessage("Cliente obrigatório");

                RuleFor(c => c.ZipCode)
                    .NotEmpty()
                    .Must(p => p.Length == 8)
                    .WithMessage("CEP deve ter 8 caracteres");

                RuleFor(c => c.Items)
                    .NotEmpty()
                    .WithMessage("Itens obrigatórios");

                RuleForEach(r => r.Items)
                    .NotEmpty()
                    .SetValidator(new CreateOrderItemCommand.CreateOrderItemCommandValidator());
            }
        }
    }
}