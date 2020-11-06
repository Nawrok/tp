using System;

namespace Store.DAL.Model
{
    public class Facture : Event
    {
        public Facture(Guid id, Client client, Offer offer, int productCount, DateTimeOffset purchaseDate) : base(id,
            client, offer, productCount, purchaseDate) { }
    }
}