using System;

namespace Store.DAL.Model
{
    public class Facture
    {
        public Facture(Guid id, Client client, Offer offer, DateTimeOffset purchaseDate)
        {
            Id = id;
            Client = client;
            Offer = offer;
            PurchaseDate = purchaseDate;
        }

        public Guid Id { get; set; }
        public Client Client { get; set; }
        public Offer Offer { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} | Client: {Client} | Offer: {Offer} | PurchaseDate: {PurchaseDate}";
        }
    }
}