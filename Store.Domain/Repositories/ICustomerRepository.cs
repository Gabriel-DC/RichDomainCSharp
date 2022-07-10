using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Domain.Entities;

namespace Store.Domain.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer? Get(string document);
    }
}