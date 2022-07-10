using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Tests.Repositories
{
    public class FakeCostumerRepository : ICustomerRepository
    {
        public Customer? Get(string document)
        {
            if (document == "12345678911")
                return new Customer("Gabriel Almeida", "gabriel@gabriel.com");

            return null;
        }
    }
}