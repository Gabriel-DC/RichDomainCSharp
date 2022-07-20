using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {

        public FakeProductRepository()
        {
            Products = new List<Product>()
            {
                new Product("Produto 01", 10, true),
                new Product("Produto 02", 10, true),
                new Product("Produto 03", 10, true),
                new Product("Produto 04", 10, false),
                new Product("Produto 05", 10, false)
            };
        }

        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Product> Get(IEnumerable<Guid>? ids)
        {
            if (ids is null)
                return new List<Product>();

            IList<Product> products = Products.ToList();
            return products;
        }

        public IEnumerable<Product> GetAll()
        {
            IList<Product> products = Products.ToList();
            return products;
        }
    }
}