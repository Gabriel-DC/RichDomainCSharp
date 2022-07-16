using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.Tests.Entities
{
    [TestClass]
    public class DiscountTests
    {
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_ideal_o_mesmo_deve_ser_valido()
        {
            var discount = new Discount(10, DateTime.Now.AddDays(5), "CUPOM10");
            Assert.IsTrue(discount.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_ja_expirado_o_mesmo_deve_ter_o_valor_igual_a_0()
        {
            var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-5), "CUPOMJUNHO10");
            Assert.AreEqual(0, expiredDiscount.Value());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_ja_expirado_o_mesmo_deve_ser_avaliado_como_expirado()
        {
            var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-5), "CUPOMJUNHO10");
            Assert.IsTrue(expiredDiscount.IsExpired());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_com_codigo_de_promocao_vazio_o_mesmo_deve_ser_invalido()
        {
            var invalidDiscount = new Discount(10, DateTime.Now, string.Empty);
            Assert.IsFalse(invalidDiscount.IsValid());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_com_codigo_de_promocao_nulo_o_mesmo_deve_ser_invalido()
        {
            var invalidDiscount = new Discount(10, DateTime.Now, null!);
            Assert.IsFalse(invalidDiscount.IsValid());
        }
    }
}