using System.Collections.Generic;
using System.Linq;

namespace LINQ.MyProduct
{
    public static class MyProductTools
    {
        public static List<MyProduct> GetMyProductsByName(string namePart)
        {
            using (MyProductDataContext dataContext = new MyProductDataContext(new ProductionDataContext()))
            {
                List<MyProduct> result = (from product in dataContext.MyProducts
                    where product.Name.Contains(namePart)
                    select product).ToList();
                return result;
            }
        }

        public static string GetMyProductVendorByProductName(string productName)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                MyProductDataContext myDataContext = new MyProductDataContext(dataContext);
                string result = (from myProduct in myDataContext.MyProducts
                    join vendor in dataContext.ProductVendor on myProduct.ProductID equals vendor.ProductID
                    where myProduct.Name == productName
                    select vendor.Vendor.Name).First();
                return result;
            }
        }

        public static List<MyProduct> GetNMyProductsFromCategory(string categoryName, int n)
        {
            using (MyProductDataContext dataContext = new MyProductDataContext(new ProductionDataContext()))
            {
                List<MyProduct> result = (from product in dataContext.MyProducts
                    where product.ProductSubcategory != null && product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                    orderby product.ProductSubcategory.Name
                    select product).Take(n).ToList();
                return result;
            }
        }
    }
}