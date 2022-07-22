using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Store.Tests.Entities
{
    [TestClass]
    public class DomainValidationTests
    {
        [TestMethod]
        [TestCategory("Domain Validation")]
        public void Dado_uma_entidade_invalida_ser_revalidada_e_a_mesma_se_tornar_valida()
        {
            var customer = new Customer("Gabriel Ameida", "");

            Assert.IsFalse(customer.Validate().IsValid);

            customer.SetEmail("gabriel@gabriel.com");

            Assert.IsTrue(customer.Revalidate().IsValid);
        }

        [TestMethod]
        [TestCategory("Domain Validation")]
        public void Ao_adicionar_uma_notificacao_manualmente_a_uma_entidade_a_mesma_deve_ser_invalida()
        {
            var customer = new Customer("Gabriel Almeida", "gabriel@gabriel.com");

            customer.AddNotification("Não gostei do nome");

            Assert.IsFalse(customer.Valid);
        }

        [TestMethod]
        [TestCategory("Domain Validation")]
        public void Ao_adicionar_uma_notificacao_manualmente_com_membro_a_uma_entidade_a_mesma_deve_ser_invalida()
        {
            var customer = new Customer("Gabriel Almeida", "gabriel@gabriel.com");

            customer.AddNotification("Não gostei do nome", "customer.name");

            Assert.IsFalse(customer.Valid);
        }

        [TestMethod]
        [TestCategory("Domain Validation")]
        public void Dado_uma_ou_mais_entidades_invalidas_ao_limpar_suas_notificacoes_o_mesmo_nao_deve_possuir_notificacoes()
        {
            var customer = new Customer("","");
            var order = new Order(null!, 0, null!);
            var product = new Product("", 0, false);

            Assert.IsTrue(customer.Validate().Results.Count > 0);
            Assert.IsTrue(order.Validate().Results.Count > 0);
            Assert.IsTrue(product.Validate().Results.Count > 0);

            DomainNotifiable.ClearAllNotifications(customer, order, product);

            Assert.AreEqual(0, customer.ValidationResults.Count);
            Assert.AreEqual(0, order.ValidationResults.Count);
            Assert.AreEqual(0, product.ValidationResults.Count);            
        }

        [TestMethod]
        [TestCategory("Domain Validation")]
        public void Dado_uma_ou_mais_entidades_invalidas_agrupar_notificoes_em_uma_entidade()
        {
            var customer = new Customer("Gabriel Almeida", "gabriel@gabriel.com");
            var invalidDiscount = new Discount(10, DateTime.Now, "");
            var order = new Order(customer, 0, invalidDiscount);

            customer.Validate();
            invalidDiscount.Validate();

            order.AgroupNotifications(customer, invalidDiscount);

            //Assert.IsTrue(order.ValidationResults.Count > 0);
            Assert.IsFalse(order.Valid);
        }

        [TestMethod]
        [TestCategory("Domain Validation")]
        public void Dado_uma_ou_mais_entidades_invalidas_validar_notificoes_em_uma_entidade_pai()
        {
            var customer = new Customer("Gabriel Almeida", "gabriel@gabriel.com");
            var invalidDiscount = new Discount(10, DateTime.Now, "");
            var order = new Order(customer, 0, invalidDiscount);

            order.ValidateEntities(customer, invalidDiscount);
            
            Assert.IsFalse(order.Valid);
        }
    }
}