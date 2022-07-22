using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer = new Customer("Gabriel", "gabriel@gabriel.com");
        private readonly Product _product = new Product("Chocolate", 10, true);
        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5), "CUPOM10");
        private readonly Discount _expiredDiscount = new Discount(10, DateTime.Now.AddDays(-5), "CUPOMJUNHO10");
        private readonly Discount _invalidDiscount = new Discount(10, DateTime.Now, "");

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_sem_itens_o_pedido_deve_ser_invalido()
        {
            var order = new Order(_customer, 0, null!);
            Assert.IsFalse(order.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_desconto_com_cupom_invalido_o_pedido_deve_ser_valido()
        {
            var order = new Order(_customer, 0, _invalidDiscount);
            order.AddItem(_product, 1);
            Assert.IsTrue(order.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_ele_deve_gerar_um_numero_com_8_caracteres()
        {
            var order = new Order(_customer, 0, _discount);
            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_seu_status_deve_ser_aguardando_pagamento()
        {
            var order = new Order(_customer, 0, _discount);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_de_um_pedido_seu_status_deve_ser_aguardando_entrega()
        {
            var order = new Order(_customer, 0, _discount);
            order.AddItem(_product, 1);
            order.Pay(order.Total());
            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_cancelado_seu_status_deve_ser_cancelado()
        {
            var order = new Order(_customer, 0, _discount);
            order.Cancel();
            Assert.AreEqual(order.Status, EOrderStatus.Canceled);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 0, _discount);
            order.AddItem(null!, 5);
            Assert.AreEqual(0, order.Items.Count);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_com_quantidade_zero_ou_menor_o_mesmo_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 0, _discount);
            order.AddItem(_product, 0);
            Assert.AreEqual(0, order.Items.Count);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_seu_total_deve_ser_50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(50, order.Total());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_expirado_o_valor_do_pedido_deve_ser_60()
        {
            var order = new Order(_customer, 0, _expiredDiscount);
            order.AddItem(_product, 6);
            Assert.AreEqual(60, order.Total());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_invalido_o_valor_do_pedido_deve_ser_60()
        {
            var order = new Order(_customer, 10, _invalidDiscount);
            order.AddItem(_product, 5);
            Assert.AreEqual(60, order.Total());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_50()
        {
            var order = new Order(_customer, 0, _discount);
            order.AddItem(_product, 6);
            Assert.AreEqual(50, order.Total());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_uma_taxa_de_entrega_de_10_o_valor_do_pedido_deve_ser_60()
        {
            var order = new Order(_customer, 10, null!);
            order.AddItem(_product, 5);
            Assert.AreEqual(60, order.Total());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_sem_cliente_o_mesmo_deve_ser_invalido()
        {
            var order = new Order(null!, 10, _discount);
            order.AddItem(_product, 5);
            Assert.IsFalse(order.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_sem_itens_o_mesmo_deve_ser_invalido()
        {
            var order = new Order(_customer, 0, _discount);
            Assert.IsFalse(order.IsValid());
        }
    }
}