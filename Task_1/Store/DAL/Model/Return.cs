using System;

namespace Store.DAL.Model
{
    public class Return : Event
    {
        public Return(Guid id, Client client, Offer offer, int productCount, DateTimeOffset purchaseDate,
            DateTimeOffset returnDate) : base(id, client, offer, productCount, purchaseDate)
        {
            ReturnDate = returnDate;
        }

        public DateTimeOffset ReturnDate { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" | ReturnDate: {ReturnDate}";
        }
    }
}