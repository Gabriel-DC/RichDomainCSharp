using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.Tests.Queries
{
    [TestClass]
    public class CustomerQueriesTests
    {
        private readonly ICustomerRepository _customerRepository = new FakeCustomerRepository();

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_um_cliente_especifico_deve_retornar_1_cliente()
        {
            var customer = _customerRepository.Get("gabriel@gabriel.com");

            Assert.IsNotNull(customer);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_um_cliente_inexistente_deve_retornar_nulo()
        {
            var customer = _customerRepository.Get("email@NaoExiste.com");

            Assert.IsNull(customer);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_um_cliente_com_email_vazio_o_mesmo_deve_retornar_nulo()
        {
            var customer = _customerRepository.Get("");

            Assert.IsNull(customer);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_um_cliente_com_email_nulo_o_mesmo_deve_retornar_nulo()
        {
            var customer = _customerRepository.Get(null!);

            Assert.IsNull(customer);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_todos_os_clientes_deve_retornar_3_clientes()
        {
            var customers = _customerRepository.GetAll().ToList();

            Assert.AreEqual(3, customers.Count);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_todos_os_clientes_deve_retornar_3_clientes_ordenados_por_nome()
        {
            var orderedByNameCustomer = _customerRepository.GetAll().OrderBy(c => c.Name);

            Assert.AreEqual("Gabriel Almeida", orderedByNameCustomer.First().Name);
            Assert.AreEqual("João Pedro", orderedByNameCustomer.Skip(1).First().Name);
            Assert.AreEqual("Maria", orderedByNameCustomer.Skip(2).First().Name);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_todos_os_clientes_deve_retornar_3_clientes_ordenados_por_email()
        {
            var orderedByEmailCustomer = _customerRepository.GetAll().OrderBy(c => c.Email);

            Assert.AreEqual("Gabriel Almeida", orderedByEmailCustomer.First().Name);
            Assert.AreEqual("João Pedro", orderedByEmailCustomer.Skip(1).First().Name);
            Assert.AreEqual("Maria", orderedByEmailCustomer.Skip(2).First().Name);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_um_cliente_por_guid_retornar_um_cliente()
        {
            var customer = _customerRepository.Get(Guid.NewGuid());
            Assert.IsNotNull(customer);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_um_cliente_por_guid_vazio_deve_retornar_nulo()
        {
            var customer = _customerRepository.Get(Guid.Empty);
            Assert.IsNull(customer);
        }
    }
}