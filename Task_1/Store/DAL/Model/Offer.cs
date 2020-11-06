using System;

namespace Store.DAL.Model
{
    public class Offer
    {
        public Offer(Guid id, Product product, decimal netPrice, decimal tax, int productsInStock)
        {
            Id = id;
            Product = product;
            NetPrice = netPrice;
            Tax = tax;
            ProductsInStock = productsInStock;
        }

        public Guid Id { get; set; }
        public Product Product { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Tax { get; set; }
        public int ProductsInStock { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} | Product: {Product.Id} | NetPrice: {NetPrice} | Tax: {Tax} | ProductsInStock: {ProductsInStock}";
        }
    }
}