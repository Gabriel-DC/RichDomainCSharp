using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.Tests.Commands
{
    [TestClass]
    public class CreateOrderCommandsTests
    {
        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand()
            {
                CustomerId = "",
                ZipCode = "13411080",
                PromoCode = "CUPOM10",
                Items = new List<CreateOrderItemCommand>()
                {
                    new CreateOrderItemCommand(Guid.NewGuid().ToString(), 1),
                    new CreateOrderItemCommand(Guid.NewGuid().ToString(), 1),
                }
            };
            command.Validate();

            Assert.IsFalse(command.IsValid);
        }
    }
}