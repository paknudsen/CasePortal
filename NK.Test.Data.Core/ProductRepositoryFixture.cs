using Newtonsoft.Json;
using NK.Data.Repositories.Entities;
using NK.Data.Repositories.Extensions;
using NK.DataRepositories;
using System;
using System.Linq.Expressions;

namespace NK.Test.Data.Core
{
    [TestFixture]
    public class ProductRepositoryFixture
    {
        [SetUp]
        public void Setup()
        {
            using (ProductRepository repo = new ProductRepository())
            {
                repo.Products.FillProductRepositoryMock();
                repo.Commit();
            }
        }

        [Test]
        public void FetchProductsAddedToDataSource_ExpectProductsToBeFound()
        {
            List<Product> products = new List<Product>();

            using(ProductRepository repo = new ProductRepository())
            {
                products = repo.Products.Get().ToList();
            }

            Assert.IsTrue(products.Any());
        }

        [Test]
        public void AddProductsAddedToDataSource_ExpectProductsToBeAdded()
        {
            List<Product> products = new List<Product>();

            var increment = 0;
            using (ProductRepository repo = new ProductRepository())
            {
               var preCount  = repo.Products.Count();

                repo.Products.Add(new Product { ProductId = 3, Name = "ProductNr 3" });
                repo.Commit();

                var postCount = repo.Products.Count();

                increment = postCount - preCount;
            }

            Assert.IsTrue(increment == 1);
        }

    }
}