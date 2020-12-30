using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public static class MyProductTools
    {
        public static List<MyProduct> GetMyProductsByName(string namePart)
        {
            using (MyProductDataContext _dataContext = new MyProductDataContext(new ProductionDataContext()))
            {
                List<MyProduct> result = (from product in _dataContext.myProducts
                                        where product.Name.Contains(namePart)
                                        select product).ToList();
                return result;
            }
        }

        public static string GetMyProductVendorByProductName(string productName)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> vendors = _dataContext.GetTable<ProductVendor>();
                MyProductDataContext _myDataContext = new MyProductDataContext(new ProductionDataContext());
                string result = (from myProduct in _myDataContext.myProducts
                                 join vendor in _dataContext.ProductVendor on myProduct.ProductID equals vendor.ProductID
                                 where myProduct.Name.Equals(productName)
                                 select vendor.Vendor.Name).First();
                return result;
            }
        }


        public static List<MyProduct> GetNMyProductsFromCategory(string categoryName, int n)
        {
            using (MyProductDataContext _dataContext = new MyProductDataContext(new ProductionDataContext()))
            {
                List<MyProduct> result = (from product in _dataContext.myProducts
                                          where product.ProductSubcategory != null && product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                                          orderby product.ProductSubcategory.Name
                                          select product).Take(n).ToList();

                return result;
            }
        }
    }
}
