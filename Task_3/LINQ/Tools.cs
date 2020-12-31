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

        public static void AddProduct(Product product)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                _dataContext.Product.InsertOnSubmit(product);
                _dataContext.SubmitChanges();
            }
        }
        
        public static Product GetProduct(int productId)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<Product> products = _dataContext.GetTable<Product>();
                List<Product> result = (from product in products
                                        where product.ProductID == productId
                                        select product).ToList();
                return result.Single();
            }
        }

        public static void UpdateProduct(Product product, int productId)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Product _testProduct = _dataContext.Product.Single(p => p.ProductID == productId);
                _testProduct.Name = product.Name;
                _testProduct.ProductNumber = product.ProductNumber;
                _testProduct.MakeFlag = product.MakeFlag;
                _testProduct.FinishedGoodsFlag = product.FinishedGoodsFlag;
                _testProduct.Color = product.Color;
                _testProduct.SafetyStockLevel = product.SafetyStockLevel;
                _testProduct.ReorderPoint = product.ReorderPoint;
                _testProduct.StandardCost = product.StandardCost;
                _testProduct.ListPrice = product.ListPrice;
                _testProduct.Size = product.Size;
                _testProduct.SizeUnitMeasureCode = product.SizeUnitMeasureCode;
                _testProduct.WeightUnitMeasureCode = product.WeightUnitMeasureCode;
                _testProduct.Weight = product.Weight;
                _testProduct.DaysToManufacture = product.DaysToManufacture;
                _testProduct.ProductLine = product.ProductLine;
                _testProduct.Class = product.Class;
                _testProduct.Style = product.Style;
                _testProduct.ProductSubcategoryID = product.ProductSubcategoryID;
                _testProduct.ProductModelID = product.ProductModelID;
                _testProduct.SellStartDate = product.SellStartDate;
                _testProduct.SellEndDate = product.SellEndDate;
                _testProduct.DiscontinuedDate = product.DiscontinuedDate;
                _testProduct.ModifiedDate = DateTime.Today;
                _dataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
        }

        public static void DeleteProduct(Product product)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Product current = _dataContext.Product.Single(p => product.ProductID == p.ProductID);
                _dataContext.Product.DeleteOnSubmit(current);
                _dataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
        }
    }
}
