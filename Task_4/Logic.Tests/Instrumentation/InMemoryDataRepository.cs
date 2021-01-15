using System;
using System.Collections.Generic;
using System.Threading;
using Data;

namespace Logic.Tests.Instrumentation
{
    internal class InMemoryDataRepository : IDataRepository
    {
        private static readonly Dictionary<int, CreditCard> Context = new Dictionary<int, CreditCard>();
        private static int _count;

        public InMemoryDataRepository()
        {
            CreditCard card1 = new CreditCard
            {
                CardNumber = "11122233344455",
                CardType = "Vista",
                ExpMonth = 6,
                ExpYear = 21
            };
            CreditCard card2 = new CreditCard
            {
                CardNumber = "55544433322211",
                CardType = "SuperiorCard",
                ExpMonth = 3,
                ExpYear = 22
            };
            AddCreditCard(card1);
            AddCreditCard(card2);
        }

        public void AddCreditCard(CreditCard creditCard)
        {
            creditCard.CreditCardID = Interlocked.Increment(ref _count);
            creditCard.ModifiedDate = DateTime.UtcNow;
            Context.Add(creditCard.CreditCardID, creditCard);
        }

        public CreditCard GetCreditCard(int creditCardId)
        {
            return Context[creditCardId];
        }

        public IEnumerable<CreditCard> GetAllCreditCards()
        {
            return Context.Values;
        }

        public void UpdateCreditCard(int creditCardId, CreditCard creditCard)
        {
            creditCard.ModifiedDate = DateTime.UtcNow;
            Context[creditCardId] = creditCard;
        }

        public void DeleteCreditCard(int creditCardId)
        {
            Context.Remove(creditCardId);
        }

        public void Dispose()
        {
            // unnecessary - in-memory implementation
        }
    }
}