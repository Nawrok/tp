using System;
using System.Collections.Generic;
using System.Threading;
using Data;

namespace Logic.Tests.Instrumentation
{
    public class InMemoryDataRepository : IDataRepository
    {
        private readonly Dictionary<int, CreditCard> _context = new Dictionary<int, CreditCard>();
        private int _count = 1000;

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
            creditCard.ModifiedDate = DateTime.Today;
            _context.Add(creditCard.CreditCardID, creditCard);
        }

        public CreditCard GetCreditCard(int creditCardId)
        {
            return _context[creditCardId];
        }

        public IEnumerable<CreditCard> GetAllCreditCards()
        {
            return _context.Values;
        }

        public void UpdateCreditCard(int creditCardId, CreditCard creditCard)
        {
            creditCard.ModifiedDate = DateTime.Today;
            _context[creditCardId] = creditCard;
        }

        public void DeleteCreditCard(int creditCardId)
        {
            _context.Remove(creditCardId);
        }

        public void Dispose()
        {
            _context.Clear();
        }
    }
}