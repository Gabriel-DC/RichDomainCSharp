using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;

namespace Store.Domain.Handlers
{
    public class OrderHandler : IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;

        public OrderHandler(
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IDiscountRepository discountRepository,
            IDeliveryFeeRepository deliveryFeeRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _discountRepository = discountRepository;
            _deliveryFeeRepository = deliveryFeeRepository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            var validation = command.Validate();
            if (!validation.IsValid)
                return new GenericCommandResult(false, "Pedido inválido", validation.Errors);

            var customer = _customerRepository.Get(command.CustomerId);
            if (customer == null)
                return new GenericCommandResult(false, "Cliente não encontrado", null!);

            var products = _productRepository.Get(command.Items.Select(i => i.ProductId));
            var discount = _discountRepository.Get(command.PromoCode);
            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);

            var order = new Order(customer, deliveryFee, discount);
            foreach (var item in command.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                    order.AddItem(product, item.Quantity);
            }

            var success = _orderRepository.Save(order);

            return new GenericCommandResult(
                success, success ? $"Pedido {order.Number} realizado com sucesso" : "Erro ao realizar o pedido",
                 success ? order : order.ValidationResults
            );
        }
    }
}
