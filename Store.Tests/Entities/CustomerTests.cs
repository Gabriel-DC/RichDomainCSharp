using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.Tests.Entities
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_cliente_com_o_nome_vazio_o_mesmo_deve_ser_invalido()
        {
            var customer = new Customer(string.Empty, "gabriel@gabriel.com");
            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_cliente_com_o_nome_nulo_o_mesmo_deve_ser_invalido()
        {
            var customer = new Customer(null!, "gabriel@gabriel.com");
            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_cliente_com_o_email_vazio_o_mesmo_deve_ser_invalido()
        {
            var customer = new Customer("Gabriel Almeida", "");
            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_cliente_com_o_email_nulo_o_mesmo_deve_ser_invalido()
        {
            var customer = new Customer("Gabriel Almeida", null!);
            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_cliente_com_o_email_e_nome_o_mesmo_deve_ser_valido()
        {
            var customer = new Customer("Gabriel Almeida", "gabriel@gabriel.com");
            Assert.IsTrue(customer.IsValid());
        }
    }
}