using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.Tests.Queries
{
    [TestClass]
    public class ProductQueriesTests
    {
        private IList<Product> _products = new List<Product>();

        public ProductQueriesTests()
        {
            _products.Add(new Product("Produto 01", 10, true));
            _products.Add(new Product("Produto 02", 10, true));
            _products.Add(new Product("Produto 03", 10, true));
            _products.Add(new Product("Produto 04", 10, false));
            _products.Add(new Product("Produto 05", 10, false));
        }


        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_ativos_retornar_3()
        {
            var products = new FakeProductRepository()
                .Get(_products.Select(p => p.Id))
                .AsQueryable()
                .Where(ProductQueries.GetActiveProduts());

            Assert.AreEqual(3, products.Count());
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_inativos_retornar_2()
        {
            var products = new FakeProductRepository()
                .Get(_products.Select(p => p.Id))
                .AsQueryable()
                .Where(ProductQueries.GetInactiveProduts());

            Assert.AreEqual(2, products.Count());
        }
    }
}