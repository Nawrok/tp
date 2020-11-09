using System;

namespace Store.DAL.Model
{
    public class Return : Event
    {
        public Return(Guid id, Facture facture, DateTimeOffset returnDate, int returnedProducts) : base(id, facture.Client, facture.Offer, returnDate)
        {
            FactureId = facture.Id;
            ReturnedProducts = returnedProducts;
            ReturnedPrice = ReturnedProducts * (facture.Offer.NetPrice + facture.Offer.NetPrice * facture.Offer.Tax);
        }

        public Guid FactureId { get; }
        public int ReturnedProducts { get; }
        public decimal ReturnedPrice { get; }

        public override string ToString()
        {
            return base.ToString() + $"FactureId: {FactureId} | ReturnedProducts: {ReturnedProducts} | ReturnedPrice: {ReturnedPrice}";
        }
    }
}