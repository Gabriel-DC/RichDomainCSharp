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
    }
}
