using System;

namespace Store.Entities
{
    public class Offer
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Tax { get; set; }
        public int Count { get; set; }

        public Offer(Guid id, Product product, decimal netPrice, decimal tax, int count)
        {
            Id = id;
            Product = product;
            NetPrice = netPrice;
            Tax = tax;
            Count = count;
        }

        public override string ToString()
        {
            return $"Id: {Id} | Product: {Product.Id} | NetPrice: {NetPrice} | Tax: {Tax} | Count: {Count}";
        }
    }
}
