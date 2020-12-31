using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LINQ;
using System.Threading;

namespace LINQ.Tests
{
    [TestClass]
    public class ToolsTest
    {
        private Product _testProduct = new Product();
        
        [TestInitialize]
        public void TestInitialize()
        {
            _testProduct.Name = "Gamakey k66";
            _testProduct.ProductNumber = "KG-K666";
            _testProduct.MakeFlag = true;
            _testProduct.FinishedGoodsFlag = true;
            _testProduct.Color = "White";
            _testProduct.SafetyStockLevel = 500;
            _testProduct.ReorderPoint = 375;
            _testProduct.StandardCost = 66.67m;
            _testProduct.ListPrice = 97.6023m;
            _testProduct.Size = "20";
            _testProduct.SizeUnitMeasureCode = "CM";
            _testProduct.WeightUnitMeasureCode = "G";
            _testProduct.Weight = 21.37m;
            _testProduct.DaysToManufacture = 1;
            _testProduct.ProductLine = "S";
            _testProduct.Class = "M";
            _testProduct.Style = "U";
            _testProduct.ProductSubcategoryID = 13;
            _testProduct.ProductModelID = 37;
            _testProduct.SellStartDate = DateTime.Today.AddDays(-30);
            _testProduct.SellEndDate = null;
            _testProduct.DiscontinuedDate = DateTime.Today.AddDays(-7);
            _testProduct.rowguid = System.Guid.NewGuid();
            _testProduct.ModifiedDate = DateTime.Today;
        }

        [TestMethod]
        public void GetProductsByNameTest()
        {
            List<Product> products = Tools.GetProductsByName("Minipump");
            Assert.AreEqual(products.Count(), 1);
            Assert.AreEqual(products[0].ProductID, 844);
            Assert.AreEqual(products[0].ProductNumber, "PU-0452");
        }

        [TestMethod]
        public void GetProductsByVendorNameTest()
        {
            List<Product> products = Tools.GetProductsByVendorName("Team Athletic Co.");
            Assert.AreEqual(products.Count(), 12);
            Assert.AreEqual(products[0].ProductNumber, "GL-H102-S");
            Assert.AreEqual(products[11].ProductNumber, "SH-W890-L");
        }

        [TestMethod]
        public void GetProductNamesByVendorNameTest()
        {
            List<string> products = Tools.GetProductNamesByVendorName("Team Athletic Co.");
            Assert.AreEqual(products.Count(), 12);
            Assert.AreEqual(products[0], "Half-Finger Gloves, S");
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
            Assert.AreEqual(products.Count(), 1);
            Assert.AreEqual(products[0].ProductNumber, "PD-M562");
        }

        [TestMethod]
        public void GetNRecentlyReviewedProductsTest()
        {
            List<Product> products = Tools.GetNRecentlyReviewedProducts(2);
            Assert.AreEqual(products.Count(), 2);
            Assert.AreEqual(products[0].ProductNumber, "PD-M562");
        }

        [TestMethod]
        public void GetNProductsFromCategoryTest()
        {
            List<Product> products = Tools.GetNProductsFromCategory("Bikes", 5);
            Assert.AreEqual(products.Count(), 5);
            Assert.AreEqual(products[0].ProductNumber, "BK-M82S-38");
            Assert.AreEqual(products[4].ProductNumber, "BK-M82B-38");
        }

        [TestMethod]
        public void GetTotalStandardCostByCategoryTest()
        {
            ProductCategory category = new ProductCategory();
            category.Name = "Bikes";

            int totalStandardCost = Tools.GetTotalStandardCostByCategory(category);
            Assert.AreEqual(totalStandardCost, 92092);
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
            List<Product> products = Tools.GetAllProducts();
            Assert.AreEqual(products.Count, 504);
            Tools.AddProduct(_testProduct);
            //Assert.AreEqual(products.Count, 505); asynchronic?
            Assert.AreEqual(Tools.GetProductsByName("Gamakey")[0].ProductNumber, "KG-K666");
        }
        
        [TestMethod]
        public void GetProductTest()
        {
            List<Product> products = Tools.GetProductsByName("Minipump");
            Product result = Tools.GetProduct(844);
            Assert.AreEqual(products[0].Name, result.Name);
            Assert.AreEqual(products[0].ProductNumber, result.ProductNumber);
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            _testProduct.Name = "TEST";
            _testProduct.ProductNumber = "TS-1337";
            Tools.UpdateProduct(_testProduct, 996);

            Assert.AreEqual(Tools.GetProductsByName("TEST")[0].Name, "TEST");
            Assert.AreEqual(Tools.GetProductsByName("TEST")[0].ProductNumber, Tools.GetProduct(996).ProductNumber);
        }

        [TestMethod]
        public void DeleteProductTest()
        {
            List<Product> products = Tools.GetAllProducts();
            Assert.AreEqual(products.Count, 505);
            Tools.DeleteProduct(Tools.GetProductsByName("Gamakey")[0]);
            //Assert.AreEqual(products.Count, 504); asynchronic?
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Tools.GetProductsByName("Gamakey")[0]);
        }
    }
}
