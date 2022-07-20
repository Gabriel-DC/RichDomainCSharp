using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.Tests.Commands
{
    [TestClass]
    public class CreateOrderItemCommandTests
    {
        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_campos_com_valor_invalido_o_mesmo_deve_ser_invalido()
        {
            var command = new CreateOrderItemCommand(Guid.Empty, 0);
            Assert.IsFalse(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_campos_com_valor_valido_o_mesmo_deve_ser_valido()
        {
            var command = new CreateOrderItemCommand(Guid.NewGuid(), 1);
            Assert.IsTrue(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_quantidade_menor_que_1_o_mesmo_deve_ser_invalido()
        {
            var command = new CreateOrderItemCommand(Guid.NewGuid(), 0);
            Assert.IsFalse(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_guid_vazio_o_mesmo_deve_ser_invalido()
        {
            var command = new CreateOrderItemCommand(Guid.Empty, 1);
            Assert.IsFalse(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_quantidade_negativa_o_mesmo_deve_ser_invalido()
        {
            var command = new CreateOrderItemCommand(Guid.NewGuid(), -1);
            Assert.IsFalse(command.Validate().IsValid);
        }
    }
}