using System;

namespace Store.DAL.Model
{
    public abstract class Event
    {
        protected Event(Guid id, Client client, Offer offer, int productCount, DateTimeOffset purchaseDate)
        {
            Id = id;
            Client = client;
            Offer = offer;
            ProductCount = productCount;
            GrossPrice = ProductCount * (Offer.NetPrice + Offer.NetPrice * Offer.Tax);
            PurchaseDate = purchaseDate;
        }

        public Guid Id { get; }
        public Client Client { get; }
        public Offer Offer { get; }
        public int ProductCount { get; }
        public decimal GrossPrice { get; }
        public DateTimeOffset PurchaseDate { get; }

        public override string ToString()
        {
            return $"Id: {Id} | Client: {Client} | Offer: {Offer} | ProductCount: {ProductCount} | GrossPrice: {GrossPrice} | PurchaseDate: {PurchaseDate}";
        }
    }
}