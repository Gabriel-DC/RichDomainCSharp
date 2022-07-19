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
        private readonly IProductRepository _productRepository = new FakeProductRepository();

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_ativos_deve_retornar_3_produtos()
        {
            var activeProducts = _productRepository.Get(new List<Guid>())
                .AsQueryable()
                .Where(ProductQueries.GetActiveProducts())
                .ToList();

            Assert.AreEqual(3, activeProducts.Count);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_inativos_deve_retornar_2_produtos()
        {
            var inactiveProducts = _productRepository.Get(new List<Guid>())
                .AsQueryable()
                .Where(ProductQueries.GetInactiveProducts())
                .ToList();

            Assert.AreEqual(2, inactiveProducts.Count);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_ativos_e_inativos_deve_retornar_5_produtos()
        {
            var activeAndInactiveProducts = _productRepository.Get(new List<Guid>())
                .AsQueryable()
                .ToList();

            Assert.AreEqual(5, activeAndInactiveProducts.Count);
        }
    }
}