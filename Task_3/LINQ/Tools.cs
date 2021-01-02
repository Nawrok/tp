using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace LINQ
{
    public static class Tools
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<Product> products = dataContext.GetTable<Product>();
                List<Product> result = (from product in products
                    where product.Name.Contains(namePart)
                    select product).ToList();
                return result;
            }
        }

        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> vendors = dataContext.GetTable<ProductVendor>();
                List<Product> result = (from product in vendors
                    where product.Vendor.Name == vendorName
                    select product.Product).ToList();
                return result;
            }
        }

        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> vendors = dataContext.GetTable<ProductVendor>();
                List<string> result = (from product in vendors
                    where product.Vendor.Name == vendorName
                    select product.Product.Name).ToList();
                return result;
            }
        }

        public static string GetProductVendorByProductName(string productName)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<ProductVendor> vendors = dataContext.GetTable<ProductVendor>();
                string result = (from product in vendors
                    where product.Product.Name == productName
                    select product.Vendor.Name).First();
                return result;
            }
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<Product> products = dataContext.GetTable<Product>();
                List<Product> result = (from product in products
                    where product.ProductReview.Count == howManyReviews
                    select product).ToList();
                return result;
            }
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<ProductReview> reviews = dataContext.GetTable<ProductReview>();
                List<Product> result = (from review in reviews
                    orderby review.ReviewDate descending
                    select review.Product).Take(howManyProducts).ToList();
                return result;
            }
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<Product> products = dataContext.GetTable<Product>();
                List<Product> result = (from product in products
                    where product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                    select product).Take(n).ToList();
                return result;
            }
        }

        public static decimal GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<Product> products = dataContext.GetTable<Product>();
                decimal totalCost = (from product in products
                    where product.ProductSubcategory.ProductCategory.Name.Equals(category.Name)
                    select product.StandardCost).ToList().Sum();
                return totalCost;
            }
        }

        public static List<Product> GetAllProducts()
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<Product> products = dataContext.GetTable<Product>();
                List<Product> result = (from product in products
                    select product).ToList();
                return result;
            }
        }

        public static void AddProduct(Product product)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                dataContext.Product.InsertOnSubmit(product);
                dataContext.SubmitChanges();
            }
        }

        public static Product GetProduct(int productId)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Table<Product> products = dataContext.GetTable<Product>();
                Product result = (from product in products
                    where product.ProductID == productId
                    select product).Single();
                return result;
            }
        }

        public static void UpdateProduct(Product product)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Product testProduct = dataContext.Product.Single(p => p.ProductID.Equals(product.ProductID));
                testProduct.Name = product.Name;
                testProduct.ProductNumber = product.ProductNumber;
                testProduct.MakeFlag = product.MakeFlag;
                testProduct.FinishedGoodsFlag = product.FinishedGoodsFlag;
                testProduct.Color = product.Color;
                testProduct.SafetyStockLevel = product.SafetyStockLevel;
                testProduct.ReorderPoint = product.ReorderPoint;
                testProduct.StandardCost = product.StandardCost;
                testProduct.ListPrice = product.ListPrice;
                testProduct.Size = product.Size;
                testProduct.SizeUnitMeasureCode = product.SizeUnitMeasureCode;
                testProduct.WeightUnitMeasureCode = product.WeightUnitMeasureCode;
                testProduct.Weight = product.Weight;
                testProduct.DaysToManufacture = product.DaysToManufacture;
                testProduct.ProductLine = product.ProductLine;
                testProduct.Class = product.Class;
                testProduct.Style = product.Style;
                testProduct.ProductSubcategoryID = product.ProductSubcategoryID;
                testProduct.ProductModelID = product.ProductModelID;
                testProduct.SellStartDate = product.SellStartDate;
                testProduct.SellEndDate = product.SellEndDate;
                testProduct.DiscontinuedDate = product.DiscontinuedDate;
                testProduct.ModifiedDate = DateTime.Today;
                dataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
        }

        public static void DeleteProduct(Product product)
        {
            using (ProductionDataContext dataContext = new ProductionDataContext())
            {
                Product current = dataContext.Product.Single(p => product.ProductID.Equals(p.ProductID));
                dataContext.Product.DeleteOnSubmit(current);
                dataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
        }
    }
}