using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeOrderRepository : IOrderRepository
    {
        public bool Save(Order order)
            => order.ClearNotifications().IsValid();
    }
}