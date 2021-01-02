using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace LINQ
{
    public static class ExtensionMethods
    {
        public static List<Product> GetProductsWithoutCategoryDeclarative(this IEnumerable<Product> products)
        {
            return (from product in products
                where product.ProductSubcategory is null
                select product).ToList();
        }

        public static List<Product> GetProductsWithoutCategoryImperative(this IEnumerable<Product> products)
        {
            return products.Where(product => !product.ProductSubcategoryID.HasValue).ToList();
        }

        public static List<Product> PaginateDeclarative(this IEnumerable<Product> products, int pageSize, int pageNumber)
        {
            return (from product in products
                select product).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        public static List<Product> PaginateImperative(this IEnumerable<Product> products, int pageSize, int pageNumber)
        {
            return products.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        public static string GetProductVendorPairDeclarative(this IEnumerable<Product> products)
        {
            string result = "";
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> productVendors = dataContext.GetTable<ProductVendor>();
                var answer = (from product in products
                    join productVendor in productVendors on product.ProductID equals productVendor.ProductID
                    where productVendor.ProductID.Equals(product.ProductID)
                    select new {ProductName = product.Name, VendorName = productVendor.Vendor.Name}).ToList();
                result = answer.Aggregate(result, (current, p) => current + p.ProductName + " - " + p.VendorName + Environment.NewLine);
            }

            return result;
        }

        public static string GetProductVendorPairImperative(this IEnumerable<Product> products)
        {
            string result = "";
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> productVendors = dataContext.GetTable<ProductVendor>();
                var answer = products.Join(productVendors,
                    product => product.ProductID,
                    productVendor => productVendor.ProductID,
                    (product, productVendor) =>
                        new {ProductName = product.Name, VendorName = productVendor.Vendor.Name}).ToList();
                result = answer.Aggregate(result, (current, p) => current + p.ProductName + " - " + p.VendorName + Environment.NewLine);
            }

            return result;
        }
    }
}