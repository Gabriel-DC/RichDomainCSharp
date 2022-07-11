using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Domain.Handlers.Interfaces
{
    public class OrderHandler : Notifiable<Notification>, IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(
            ICustomerRepository customerRepository,
            IDeliveryFeeRepository deliveryFeeRepository,
            IDiscountRepository discountRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _deliveryFeeRepository = deliveryFeeRepository;
            _discountRepository = discountRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new GenericCommandResult(false, "Pedido inválido", null!);

            var customer = _customerRepository.Get(command.CustomerId!);
            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode!);
            var discount = _discountRepository.Get(command.PromoCode ?? "");

            var products = _productRepository
                .Get(command.Items.Select(i => new Guid(i.ProductId!)))
                .ToList();

            var order = new Order(customer!, deliveryFee, discount!);
            foreach (var item in command.Items)
            {
                var product = products.FirstOrDefault(p => p.Id.ToString() == item.ProductId);
                order.AddItem(product!, item.Quantity!.Value);
            }

            AddNotifications(order.Notifications);

            if (!IsValid)
                return new GenericCommandResult(false, "Falha ao gerar pedido", Notifications);

            _orderRepository.Save(order);

            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso!", order);
        }
    }
}