using System.Linq.Expressions;
using Store.Domain.Entities;

namespace Store.Domain.Queries
{
    public static class ProductQueries
    {
        public static Expression<Func<Product, bool>> GetActiveProduts()
        {
            return p => p.Active;
        }

        public static Expression<Func<Product, bool>> GetInactiveProduts()
        {
            return p => !p.Active;
        }
    }
}