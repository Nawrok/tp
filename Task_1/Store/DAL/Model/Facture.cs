using System;

namespace Store.DAL.Model
{
    public class Facture : Event
    {
        public Facture(Guid id, Client client, Offer offer, DateTimeOffset purchaseDate, int boughtProducts) : base(id, client, offer, purchaseDate)
        {
            BoughtProducts = boughtProducts;
            GrossPrice = BoughtProducts * (Offer.NetPrice + Offer.NetPrice * Offer.Tax);
        }

        public int BoughtProducts { get; }
        public decimal GrossPrice { get; }

        public override string ToString()
        {
            return base.ToString() + $" | BoughtProducts: {BoughtProducts} | GrossPrice: {GrossPrice}";
        }
    }
}