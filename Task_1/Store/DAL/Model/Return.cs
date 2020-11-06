using System;

namespace Store.DAL.Model
{
    public class Return : Event
    {
        public Return(Guid id, Client client, Offer offer, DateTimeOffset purchaseDate, DateTimeOffset returnDate) : base(id, client, offer, purchaseDate)
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
