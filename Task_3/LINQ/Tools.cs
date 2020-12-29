using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public static class Tools
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<Product> products = _dataContext.GetTable<Product>();
                List<Product> result = (from product in products
                                        where product.Name.Contains(namePart)
                                        select product).ToList();
                return result;
            }
        }

        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> vendors = _dataContext.GetTable<ProductVendor>();
                List<Product> result = (from product in vendors
                                        where product.Vendor.Name.Equals(vendorName)
                                        select product.Product).ToList();
                return result;
            }
        }

        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> vendors = _dataContext.GetTable<ProductVendor>();
                List<string> result = (from product in vendors
                                       where product.Vendor.Name.Equals(vendorName)
                                       select product.Product.Name).ToList();
                return result;
            }
        }

        public static string GetProductVendorByProductName(string productName)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> vendors = _dataContext.GetTable<ProductVendor>();
                string result = (from product in vendors
                                 where product.Product.Name.Equals(productName)
                                 select product.Vendor.Name).First();
                return result;
            }
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<Product> products = _dataContext.GetTable<Product>();
                List<Product> result = (from product in products
                                        where product.ProductReview.Count == howManyReviews
                                        select product).ToList();

                return result;
            }
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<ProductReview> reviews = _dataContext.GetTable<ProductReview>();
                List<Product> result = (from review in reviews
                                          orderby review.ReviewDate descending
                                          select review.Product
                                          ).Take(howManyProducts).ToList();
                return result;
            }
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<Product> products = _dataContext.GetTable<Product>();
                List<Product> result = (from product in products
                                          where product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                                          select product).Take(n).ToList();

                return result;
            }
        }

        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<Product> products = _dataContext.GetTable<Product>();
                decimal totalCost = (from product in products
                               where product.ProductSubcategory.ProductCategory.Name.Equals(category.Name)
                               select product.StandardCost).ToList().Sum();
                return (int)totalCost;
            }
        }

        public static List<Product> GetAllProducts()
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<Product> products = _dataContext.GetTable<Product>();
                List<Product> result = (from product in products
                                        select product).ToList();
                return result;
            }
        }
    }
}
