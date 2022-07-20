using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Handlers;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _orderRepository = new FakeOrderRepository();
            _productRepository = new FakeProductRepository();
            _discountRepository = new FakeDiscountRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_criado()
        {
            var command = new CreateOrderCommand(
                customerId: Guid.Empty,
                promoCode: "CUPOM10",
                zipCode: "12345678",
                items: new List<CreateOrderItemCommand>()
                {
                    new CreateOrderItemCommand(Guid.Empty, 1)
                }
            );

            var handler = new OrderHandler(
                _customerRepository,
                _orderRepository,
                _productRepository,
                _discountRepository,
                _deliveryFeeRepository
            );

            var result = handler.Handle(command);
            Assert.IsFalse(command.Validate().IsValid);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_existente_e_um_produto_inexistente_o_pedido_nao_deve_ser_criado()
        {
            var command = new CreateOrderCommand(
                customerId: Guid.NewGuid(),
                promoCode: "CUPOM10",
                zipCode: "12345678",
                items: new List<CreateOrderItemCommand>()
                {
                    new CreateOrderItemCommand(Guid.Empty, 1)
                }
            );

            var handler = new OrderHandler(
                _customerRepository,
                _orderRepository,
                _productRepository,
                _discountRepository,
                _deliveryFeeRepository
            );

            var result = handler.Handle(command);
            Assert.IsFalse(command.Validate().IsValid);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_existente_e_um_produto_existente_e_um_cupom_de_desconto_inexistente_o_pedido_deve_ser_criado()
        {
            var command = new CreateOrderCommand(
                customerId: Guid.NewGuid(),
                promoCode: "CUPOM1000",
                zipCode: "12345678",
                items: new List<CreateOrderItemCommand>()
                {
                    new CreateOrderItemCommand(_productRepository.Products.First().Id, 1)
                }
            );

            var handler = new OrderHandler(
                _customerRepository,
                _orderRepository,
                _productRepository,
                _discountRepository,
                _deliveryFeeRepository
            );

            var result = handler.Handle(command);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_existente_e_um_produto_existente_e_um_cupom_de_desconto_existente_o_pedido_deve_ser_criado()
        {
            var command = new CreateOrderCommand(
                customerId: Guid.NewGuid(),
                promoCode: "CUPOM10",
                zipCode: "12345678",
                items: new List<CreateOrderItemCommand>()
                {
                    new CreateOrderItemCommand(_productRepository.Products.First().Id, 1)
                }
            );

            var handler = new OrderHandler(
                _customerRepository,
                _orderRepository,
                _productRepository,
                _discountRepository,
                _deliveryFeeRepository
            );

            var result = handler.Handle(command);
            var order = result.Data as Order;

            Assert.IsNotNull(order?.Discount);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_existente_e_um_produto_existente_e_um_cupom_de_desconto_existente_e_sem_itens_o_pedido_nao_deve_ser_criado()
        {
            var command = new CreateOrderCommand(
                customerId: Guid.NewGuid(),
                promoCode: "CUPOM10",
                zipCode: "12345678",
                items: new List<CreateOrderItemCommand>()
            );

            var handler = new OrderHandler(
                _customerRepository,
                _orderRepository,
                _productRepository,
                _discountRepository,
                _deliveryFeeRepository
            );

            var result = handler.Handle(command);
            Assert.IsFalse(command.Validate().IsValid);
            Assert.IsFalse(result.Success);
        }
    }
}