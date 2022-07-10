using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Tests.Repositories
{
    public class FakeDiscountRepository : IDiscountRepository
    {
        public Discount? Get(string code)
        {
            if (code == "CUPOM10")
                return new Discount(10, DateTime.Now.AddDays(5), code);
            else if (code == "CUPOMJUNHO10")
                return new Discount(10, DateTime.Now.AddDays(-5), code);

            return null;
        }
    }
}