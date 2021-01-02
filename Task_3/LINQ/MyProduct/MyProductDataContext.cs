using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.MyProduct
{
    public class MyProductDataContext : IDisposable
    {
        public MyProductDataContext(ProductionDataContext productionDataContext)
        {
            MyProducts = productionDataContext.GetTable<Product>().Select(product => new MyProduct(product)).ToList();
        }

        public List<MyProduct> MyProducts { get; }

        public void Dispose()
        {
            MyProducts.Clear();
        }
    }
}