using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;

namespace Store.Tests.Commands
{
    [TestClass]
    public class CreateOrderCommandTests
    {
        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_campos_vazio_o_mesmo_deve_ser_invalido()
        {
            var command = new CreateOrderCommand(Guid.Empty, string.Empty, string.Empty, new List<CreateOrderItemCommand>());
            Assert.IsFalse(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_campos_com_valor_invalido_o_mesmo_deve_ser_invalido()
        {
            var command = new CreateOrderCommand(Guid.Empty, "", "", new List<CreateOrderItemCommand>());
            Assert.IsFalse(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_campos_com_valor_valido_o_mesmo_deve_ser_valido()
        {
            var command = new CreateOrderCommand(
                Guid.NewGuid(),
                "12345678",
                "",
                new List<CreateOrderItemCommand>()
                {
                    new CreateOrderItemCommand(Guid.NewGuid(), 1 ),
                    new CreateOrderItemCommand(Guid.NewGuid(), 2)
                });
            Assert.IsTrue(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_itens_vazio_o_mesmo_deve_ser_invalido()
        {
            var command = new CreateOrderCommand(
                Guid.NewGuid(),
                "12345678",
                "",
                new List<CreateOrderItemCommand>());
            Assert.IsFalse(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_itens_com_valor_invalido_o_mesmo_deve_ser_invalido()
        {
            var command = new CreateOrderCommand(
                Guid.NewGuid(),
                "12345678",
                "",
                new List<CreateOrderItemCommand>()
                {
                    new CreateOrderItemCommand(Guid.NewGuid(), 0 ),
                    new CreateOrderItemCommand(Guid.NewGuid(), -1)
                });
            Assert.IsFalse(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_apenas_um_item_valido_o_mesmo_deve_ser_valido()
        {
            var command = new CreateOrderCommand(
                Guid.NewGuid(),
                "12345678",
                "",
                new List<CreateOrderItemCommand>()
                {
                    new CreateOrderItemCommand(Guid.NewGuid(), 1)
                });
            Assert.IsTrue(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_com_apenas_um_item_valido_e_outro_invalido_o_mesmo_deve_ser_invalido()
        {
            var command = new CreateOrderCommand(
                Guid.NewGuid(),
                "12345678",
                "",
                new List<CreateOrderItemCommand>()
                {
                    new CreateOrderItemCommand(Guid.NewGuid(), 1),
                    new CreateOrderItemCommand(Guid.NewGuid(), 0)
                });
            Assert.IsFalse(command.Validate().IsValid);
        }
    }
}