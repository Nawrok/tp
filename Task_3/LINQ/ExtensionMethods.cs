using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public static class ExtensionMethods
    {
        public static List<Product> GetProductsWithoutCategoryDeclarative(this List<Product> products)
        {
            return (from product in products
                    where product.ProductSubcategory is null
                    select product).ToList<Product>();
        }

        public static List<Product> GetProductsWithNoCategoryImperative(this List<Product> products)
        {
            return products.Where(product => product.ProductSubcategoryID.Equals(null)).ToList();
        }

        public static List<Product> PaginateDeclarative(this List<Product> products, int pageSize, int pageNumber)
        {
            return (from product in products
                    select product).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        public static List<Product> PaginateImperative(this List<Product> products, int pageSize, int pageNumber)
        {
            return products.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        public static string GetProductVendorPairDeclarative(this List<Product> products)
        {
            string result = "";
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> productVendors = _dataContext.GetTable<ProductVendor>();
                var answer = (from product in products
                              join productVendor in productVendors on product.ProductID equals productVendor.ProductID
                              where productVendor.ProductID.Equals(product.ProductID)
                              select new { ProductName = product.Name, VendorName = productVendor.Vendor.Name }).ToList();
                foreach (var p in answer)
                {
                    result += p.ProductName + "-" + p.VendorName + "\n";
                }
            }
            return result;
        }

        public static string GetProductVendorPairImperative(this List<Product> products)
        {
            string result = "";
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> productVendors = _dataContext.GetTable<ProductVendor>();
                var answer = products.Join(productVendors,
                                                    product => product.ProductID,
                                                    productVendor => productVendor.ProductID,
                                                    (product, productVendor) => 
                                                    new { ProductName = product.Name, VendorName = productVendor.Vendor.Name }).ToList();
                foreach (var p in answer)
                {
                    result += p.ProductName + "-" + p.VendorName + "\n";
                }
            }
            return result;
        }
    }
}
