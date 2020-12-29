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
    }
}
