using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customer? Get(string email);
        Customer? Get(Guid id);
        IEnumerable<Customer> GetAll();

        public IEnumerable<Customer> Customers { get; set; }
    }
}