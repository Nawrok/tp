using System;

namespace Store.DAL.Model
{
    public class Return : Event
    {
        public Return(Facture facture, DateTimeOffset returnDate) : base(facture.Id, facture.Client, facture.Offer, facture.ProductCount, facture.PurchaseDate)
        {
            ReturnDate = returnDate;
        }

        public DateTimeOffset ReturnDate { get; }

        public override string ToString()
        {
            return base.ToString() + $" | ReturnDate: {ReturnDate}";
        }
    }
}