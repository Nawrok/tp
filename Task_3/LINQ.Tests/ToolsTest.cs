using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LINQ.Tests
{
    [TestClass]
    public class ToolsTest
    {
        private Product _testProduct;

        [TestInitialize]
        public void TestInitialize()
        {
            _testProduct = new Product
            {
                Name = "Gamakey k66",
                ProductNumber = "KG-K666",
                MakeFlag = true,
                FinishedGoodsFlag = true,
                Color = "White",
                SafetyStockLevel = 500,
                ReorderPoint = 375,
                StandardCost = 66.67m,
                ListPrice = 97.6023m,
                Size = "20",
                SizeUnitMeasureCode = "CM",
                WeightUnitMeasureCode = "G",
                Weight = 21.37m,
                DaysToManufacture = 1,
                ProductLine = "S",
                Class = "M",
                Style = "U",
                ProductSubcategoryID = 13,
                ProductModelID = 37,
                SellStartDate = DateTime.Today.AddDays(-30),
                SellEndDate = null,
                DiscontinuedDate = DateTime.Today.AddDays(-7),
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Today
            };
        }

        [TestMethod]
        public void GetProductsByNameTest()
        {
            List<Product> products = Tools.GetProductsByName("Minipump");
            Assert.AreEqual(products.Count, 1);
            Assert.AreEqual(products.First().ProductID, 844);
            Assert.AreEqual(products.First().ProductNumber, "PU-0452");
        }

        [TestMethod]
        public void GetProductsByVendorNameTest()
        {
            List<Product> products = Tools.GetProductsByVendorName("Team Athletic Co.");
            Assert.AreEqual(products.Count, 12);
            Assert.AreEqual(products.First().ProductNumber, "GL-H102-S");
            Assert.AreEqual(products[11].ProductNumber, "SH-W890-L");
        }

        [TestMethod]
        public void GetProductNamesByVendorNameTest()
        {
            List<string> products = Tools.GetProductNamesByVendorName("Team Athletic Co.");
            Assert.AreEqual(products.Count, 12);
            Assert.AreEqual(products.First(), "Half-Finger Gloves, S");
            Assert.AreEqual(products[11], "Women's Mountain Shorts, L");
        }

        [TestMethod]
        public void GetProductVendorByProductNameTest()
        {
            string vendors = Tools.GetProductVendorByProductName("Minipump");
            Assert.AreEqual(vendors, "International Trek Center");
        }

        [TestMethod]
        public void GetProductsWithNRecentReviewsTest()
        {
            List<Product> products = Tools.GetProductsWithNRecentReviews(2);
            Assert.AreEqual(products.Count, 1);
            Assert.AreEqual(products.First().ProductNumber, "PD-M562");
        }

        [TestMethod]
        public void GetNRecentlyReviewedProductsTest()
        {
            List<Product> products = Tools.GetNRecentlyReviewedProducts(2);
            Assert.AreEqual(products.Count, 2);
            Assert.AreEqual(products.First().ProductNumber, "PD-M562");
        }

        [TestMethod]
        public void GetNProductsFromCategoryTest()
        {
            List<Product> products = Tools.GetNProductsFromCategory("Bikes", 5);
            Assert.AreEqual(products.Count, 5);
            Assert.AreEqual(products.First().ProductNumber, "BK-M82S-38");
            Assert.AreEqual(products[4].ProductNumber, "BK-M82B-38");
        }

        [TestMethod]
        public void GetTotalStandardCostByCategoryTest()
        {
            ProductCategory category = new ProductCategory {Name = "Bikes"};
            decimal totalStandardCost = Tools.GetTotalStandardCostByCategory(category);
            Assert.AreEqual(totalStandardCost, 92092.8230m);
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            List<Product> products = Tools.GetAllProducts();
            Assert.AreEqual(products.Count, 504);
        }

        [TestMethod]
        public void AddProductTest()
        {
            Assert.AreEqual(Tools.GetAllProducts().Count, 504);
            Tools.AddProduct(_testProduct);
            Assert.AreEqual(Tools.GetAllProducts().Count, 505);
            Assert.AreEqual(Tools.GetProductsByName(_testProduct.Name).First().ProductNumber, "KG-K666");
            Tools.DeleteProduct(_testProduct);
            Assert.AreEqual(Tools.GetAllProducts().Count, 504);
        }

        [TestMethod]
        public void GetProductTest()
        {
            List<Product> products = Tools.GetProductsByName("Minipump");
            Product result = Tools.GetProduct(844);
            Assert.AreEqual(products.First().Name, result.Name);
            Assert.AreEqual(products.First().ProductNumber, result.ProductNumber);
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            Product toUpdate = Tools.GetProductsByName("Minipump").Single();
            Product toRestore = Tools.GetProductsByName("Minipump").Single();
            toUpdate.Name = "TEST";
            toUpdate.ProductNumber = "TS-1337";
            Tools.UpdateProduct(toUpdate);
            Assert.AreEqual(Tools.GetProductsByName(toUpdate.Name).First().Name, toUpdate.Name);
            Assert.AreEqual(Tools.GetProductsByName(toUpdate.Name).First().ProductNumber, toUpdate.ProductNumber);

            Tools.UpdateProduct(toRestore);
            Assert.AreEqual(Tools.GetProductsByName(toRestore.Name).First().Name, toRestore.Name);
            Assert.AreEqual(Tools.GetProductsByName(toRestore.Name).First().ProductNumber, toRestore.ProductNumber);
        }

        [TestMethod]
        public void DeleteProductTest()
        {
            Tools.AddProduct(_testProduct);
            Assert.AreEqual(Tools.GetAllProducts().Count, 505);
            Tools.DeleteProduct(_testProduct);
            Assert.AreEqual(Tools.GetAllProducts().Count, 504);
            Assert.ThrowsException<InvalidOperationException>(() => Tools.GetProductsByName(_testProduct.Name).First());
        }
    }
}