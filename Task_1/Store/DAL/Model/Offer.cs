using System;

namespace Store.DAL.Model
{
    public class Offer
    {
        public Offer(Product product, decimal netPrice, decimal tax, int productsInStock)
        {
            Product = product;
            NetPrice = netPrice;
            Tax = tax;
            ProductsInStock = productsInStock;
        }

        public Product Product { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Tax { get; set; }
        public int ProductsInStock { get; set; }

        public override string ToString()
        {
            return $"Product: {Product.Id} | NetPrice: {NetPrice} | Tax: {Tax} | ProductsInStock: {ProductsInStock}";
        }
    }
}