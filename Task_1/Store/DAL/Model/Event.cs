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
        public Client Client { get; set; }
        public Offer Offer { get; set; }
        public int ProductCount { get; set; }
        public decimal GrossPrice { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} | Client: {Client} | Offer: {Offer} | ProductCount: {ProductCount} | GrossPrice: {GrossPrice} | PurchaseDate: {PurchaseDate}";
        }
    }
}