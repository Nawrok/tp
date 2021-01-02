using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LINQ.Tests
{
    [TestClass]
    public class ExtensionMethodsTests
    {
        [TestMethod]
        public void GetProductsWithoutCategoryDeclarativeTest()
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                List<Product> results = products.GetProductsWithoutCategoryDeclarative();

                Assert.AreEqual(results.Count, 209);
                foreach (Product product in results)
                {
                    Assert.IsNull(product.ProductSubcategory);
                }
            }
        }

        [TestMethod]
        public void GetProductsWithNoCategoryImperativeTest()
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                List<Product> results = products.GetProductsWithoutCategoryImperative();

                Assert.AreEqual(results.Count, 209);
                foreach (Product product in results)
                {
                    Assert.IsNull(product.ProductSubcategory);
                }
            }
        }

        [TestMethod]
        public void GetProductsWithNoCategoryTest()
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                List<Product> resultsD = products.GetProductsWithoutCategoryDeclarative();
                List<Product> resultsI = products.GetProductsWithoutCategoryImperative();

                Assert.AreSame(resultsD.First(), resultsI.First());
                Assert.AreEqual(resultsD.First().ProductNumber, resultsI.First().ProductNumber);
                Assert.AreEqual(resultsD.First().ProductNumber, "AR-5381");
            }
        }

        [TestMethod]
        public void PaginateDeclarativeTest()
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                List<Product> results = products.PaginateDeclarative(5, 5);

                Assert.AreEqual(results.Count, 5);
                for (int i = 0; i < 5; i++)
                {
                    Assert.AreEqual(products[i + 20].Name, results[i].Name);
                }

                Assert.ThrowsException<ArgumentOutOfRangeException>(() => results[5]);
            }
        }

        [TestMethod]
        public void PaginateImperativeTest()
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                List<Product> results = products.PaginateImperative(5, 5);

                Assert.AreEqual(results.Count, 5);
                for (int i = 0; i < 5; i++)
                {
                    Assert.AreEqual(products[i + 20].Name, results[i].Name);
                }

                Assert.ThrowsException<ArgumentOutOfRangeException>(() => results[5]);
            }
        }

        [TestMethod]
        public void GetProductVendorPairDeclarativeTest()
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                string result = products.GetProductVendorPairDeclarative();
                string pair = result.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[10];

                Assert.AreEqual(pair, "Chainring Bolts - Training Systems");
                Assert.IsTrue(result.Contains("Hex Nut 16 - Mountain Works"));
                Assert.IsTrue(result.Contains("Thin-Jam Lock Nut 2 - Australia Bike Retailer"));
            }
        }

        [TestMethod]
        public void GetProductVendorPairImperativeTest()
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                string result = products.GetProductVendorPairImperative();
                string pair = result.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[10];

                Assert.AreEqual(pair, "Chainring Bolts - Training Systems");
                Assert.IsTrue(result.Contains("Hex Nut 16 - Mountain Works"));
                Assert.IsTrue(result.Contains("Thin-Jam Lock Nut 2 - Australia Bike Retailer"));
            }
        }
    }
}