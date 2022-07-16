using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.Tests.Entities
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_produto_com_titulo_vazio_o_mesmo_deve_ser_invalido()
        {
            var productTitleless = new Product(string.Empty, 10, true);
            Assert.IsFalse(productTitleless.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_produto_com_titulo_nulo_o_mesmo_deve_ser_invalido()
        {
            var productTitleless = new Product(null!, 10, true);
            Assert.IsFalse(productTitleless.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_produto_com_valor_igual_a_zero_o_mesmo_deve_ser_invalido()
        {
            var productTitleless = new Product("Chocolate", 0, true);
            Assert.IsFalse(productTitleless.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_produto_ideal_o_mesmo_deve_ser_valido()
        {
            var productTitleless = new Product("Chocolate", 10.99m, true);
            Assert.IsTrue(productTitleless.IsValid());
        }
    }
}