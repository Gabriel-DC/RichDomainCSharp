using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeDiscountRepository : IDiscountRepository
    {
        public Discount? Get(string code)
        {
            if (code == "CUPOM10")
                return new Discount(10, DateTime.Now.AddDays(5), "CUPOM10");
            else if (code == "CUPOMJULHO10")
                return new Discount(10, DateTime.Now.AddMonths(-1), "CUPOMJULHO10");
            else
                return null;
        }
    }
}