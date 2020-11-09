using System;

namespace Store.DAL.Model
{
    public abstract class Event
    {
        protected Event(Guid id, Client client, Offer offer, DateTimeOffset date)
        {
            Id = id;
            Client = client;
            Offer = offer;
            Date = date;
        }

        public Guid Id { get; }
        public Client Client { get; }
        public Offer Offer { get; }
        public DateTimeOffset Date { get; }

        public override string ToString()
        {
            return $"Id: {Id} | Client: {Client} | Offer: {Offer} | Date: {Date}";
        }
    }
}