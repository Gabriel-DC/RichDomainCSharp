using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Domain.Repositories
{
    public interface IDeliveryFeeRepository
    {
        decimal Get(string zipCode);
    }
}