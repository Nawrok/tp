using System;

namespace Store.Entities
{
    public class Offer
    {
        public Offer() { }

        public Offer(Guid id, Product product, decimal price, int count)
        {
            Id = id;
            Product = product;
            Price = price;
            Count = count;
        }

        public Guid Id { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} | Product: {Product.Id} | Price: {Price} | Count: {Count}";
        }
    }
}
