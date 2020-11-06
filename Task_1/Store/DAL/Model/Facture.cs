using System;

namespace Store.DAL.Model
{
    public class Facture : Event
    {
        public Facture(Guid id, Client client, Offer offer, DateTimeOffset purchaseDate) : base(id, client, offer, purchaseDate)
        {

        }
    }
}