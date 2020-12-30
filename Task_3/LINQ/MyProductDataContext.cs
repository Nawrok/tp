using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public class MyProductDataContext : IDisposable
    {
        public List<MyProduct> myProducts { get; set; }

        public MyProductDataContext(ProductionDataContext productionDataContext)
        {
            myProducts = productionDataContext.GetTable<Product>().Select(product => new MyProduct(product)).ToList();
        }

        public void Dispose()
        {
            myProducts.Clear();
        }
    }
}
