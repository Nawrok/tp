using Microsoft.VisualStudio.TestTools.UnitTesting;
using LINQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Tests
{
    [TestClass]
    public class ExtensionMethodsTests
    {
        [TestMethod]
        public void GetProductsWithoutCategoryDeclarativeTest()
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                List<Product> products = _dataContext.GetTable<Product>().ToList();
                List<Product> results = products.GetProductsWithoutCategoryDeclarative();

                Assert.AreEqual(results.Count(), 209);
                foreach (Product product in results)
                {
                    Assert.AreEqual(product.ProductSubcategory, null);
                }
            }
        }

        [TestMethod]
        public void GetProductsWithNoCategoryImperativeTest()
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                List<Product> products = _dataContext.GetTable<Product>().ToList();
                List<Product> results = products.GetProductsWithoutCategoryImperative();

                Assert.AreEqual(results.Count(), 209);
                foreach (Product product in results)
                {
                    Assert.AreEqual(product.ProductSubcategory, null);
                }
            }
        }

        [TestMethod]
        public void GetProductsWithNoCategoryTest()
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                List<Product> products = _dataContext.GetTable<Product>().ToList();
                List<Product> resultsD = products.GetProductsWithoutCategoryDeclarative();
                List<Product> resultsI = products.GetProductsWithoutCategoryImperative();

                Assert.AreSame(resultsD[0], resultsI[0]);
                Assert.AreEqual(resultsD[0].ProductNumber, resultsI[0].ProductNumber);
                Assert.AreEqual(resultsD[0].ProductNumber, "AR-5381");
            }
        }

        [TestMethod]
        public void PaginateDeclarativeTest()
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                List<Product> products = _dataContext.GetTable<Product>().ToList();
                List<Product> results = products.PaginateDeclarative(5, 5);

                Assert.AreEqual(results.Count, 5);
                Assert.AreEqual(products[20].Name, results[0].Name);
                Assert.AreEqual(products[21].Name, results[1].Name);
                Assert.AreEqual(products[22].Name, results[2].Name);
                Assert.AreEqual(products[23].Name, results[3].Name);
                Assert.AreEqual(products[24].Name, results[4].Name);
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => results[5]);
            }
        }

        [TestMethod]
        public void PaginateImperativeTest()
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                List<Product> products = _dataContext.GetTable<Product>().ToList();
                List<Product> results = products.PaginateImperative(5, 5);

                Assert.AreEqual(results.Count, 5);
                Assert.AreEqual(products[20].Name, results[0].Name);
                Assert.AreEqual(products[21].Name, results[1].Name);
                Assert.AreEqual(products[22].Name, results[2].Name);
                Assert.AreEqual(products[23].Name, results[3].Name);
                Assert.AreEqual(products[24].Name, results[4].Name);
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => results[5]);
            }
        }

        [TestMethod]
        public void GetProductVendorPairDeclarativeTest()
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                List<Product> products = _dataContext.GetTable<Product>().ToList();
                string result = products.GetProductVendorPairDeclarative();
                string pair = result.Split('\n')[10];

                Assert.AreEqual(pair, "Chainring Bolts - Training Systems");
                Assert.IsTrue(result.Contains("Hex Nut 16 - Mountain Works"));
                Assert.IsTrue(result.Contains("Thin-Jam Lock Nut 2 - Australia Bike Retailer"));
            }
        }

        [TestMethod]
        public void GetProductVendorPairImperativeTest()
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                List<Product> products = _dataContext.GetTable<Product>().ToList();
                string result = products.GetProductVendorPairImperative();
                string pair = result.Split('\n')[10];

                Assert.AreEqual(pair, "Chainring Bolts - Training Systems");
                Assert.IsTrue(result.Contains("Hex Nut 16 - Mountain Works"));
                Assert.IsTrue(result.Contains("Thin-Jam Lock Nut 2 - Australia Bike Retailer"));
            }
        }
    }
}