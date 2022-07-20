using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer? Get(string email) =>
            email == "gabriel@gabriel.com"
                ? new Customer("Gabriel Almeida", "gabriel@gabriel.com")
                : null;

        public Customer? Get(Guid id) =>
            id != Guid.Empty
                ? new Customer("Gabriel Almeida", "gabriel@gabriel.com")
                : null;

        public IEnumerable<Customer> GetAll()
        {
            return new List<Customer>
            {
                new Customer("João Pedro", "joao@joao.com"),
                new Customer("Gabriel Almeida", "gabriel@gabriel.com"),
                new Customer("Maria", "maria@maria.com")
            };
        }
    }
}
