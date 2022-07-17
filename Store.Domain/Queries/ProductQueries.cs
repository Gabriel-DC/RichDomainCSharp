using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Store.Domain.Entities;

namespace Store.Domain.Queries
{
    public static class ProductQueries
    {
        public static Expression<Func<Product, bool>> GetActiveProducts()
            => (x => x.Active);

        public static Expression<Func<Product, bool>> GetInactiveProducts()
            => (x => !x.Active);
    }
}