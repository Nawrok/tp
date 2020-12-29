using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LINQ;

namespace LINQ.Tests
{
    [TestClass]
    public class ToolsTest
    {
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
    }
}
