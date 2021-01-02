using System.Collections.Generic;
using LINQ.MyProduct;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LINQ.Tests
{
    [TestClass]
    public class MyProductToolsTests
    {
        [TestMethod]
        public void GetMyProductsByNameTest()
        {
            List<MyProduct.MyProduct> myProducts = MyProductTools.GetMyProductsByName("Minipump");
            Assert.AreEqual(myProducts.Count, 1);
            Assert.AreEqual(myProducts[0].ProductID, 844);
            Assert.AreEqual(myProducts[0].ProductNumber, "PU-0452");
        }

        [TestMethod]
        public void GetMyProductVendorByProductNameTest()
        {
            string vendors = MyProductTools.GetMyProductVendorByProductName("Minipump");
            Assert.AreEqual(vendors, "International Trek Center");
        }

        [TestMethod]
        public void GetNMyProductsFromCategoryTest()
        {
            List<MyProduct.MyProduct> myProducts = MyProductTools.GetNMyProductsFromCategory("Bikes", 5);
            Assert.AreEqual(myProducts.Count, 5);
            Assert.AreEqual(myProducts[0].ProductNumber, "BK-M82S-38");
            Assert.AreEqual(myProducts[4].ProductNumber, "BK-M82B-38");
        }
    }
}