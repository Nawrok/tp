using System;

namespace Store.DAL.Model
{
    public class Return : Event
    {
        public Return(Guid id, Client client, Offer offer, DateTimeOffset returnDate, int returnedProducts) : base(id, client, offer, returnDate)
        {
            ReturnedProducts = returnedProducts;
            ReturnedPrice = ReturnedProducts * (Offer.NetPrice + Offer.NetPrice * Offer.Tax);
        }

        public int ReturnedProducts { get; }
        public decimal ReturnedPrice { get; }

        public override string ToString()
        {
            return base.ToString() + $" | ReturnedProducts: {ReturnedProducts} | ReturnedPrice: {ReturnedPrice}";
        }
    }
}