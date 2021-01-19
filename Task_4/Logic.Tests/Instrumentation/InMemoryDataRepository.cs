using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Data;

namespace Logic.Tests.Instrumentation
{
    public class InMemoryDataRepository : IDataRepository
    {
        private static int _count;
        private readonly Dictionary<int, Data.CreditCard> _context = new Dictionary<int, Data.CreditCard>();

        public InMemoryDataRepository()
        {
            Data.CreditCard card1 = new Data.CreditCard
            {
                CardNumber = "11122233344455",
                CardType = "Vista",
                ExpMonth = 6,
                ExpYear = 21
            };
            Data.CreditCard card2 = new Data.CreditCard
            {
                CardNumber = "55544433322211",
                CardType = "SuperiorCard",
                ExpMonth = 3,
                ExpYear = 22
            };
            AddCreditCard(card1);
            AddCreditCard(card2);
        }

        public void AddCreditCard(Data.CreditCard creditCard)
        {
            creditCard.CreditCardID = Interlocked.Increment(ref _count);
            creditCard.ModifiedDate = DateTime.UtcNow;
            _context.Add(creditCard.CreditCardID, creditCard);
        }

        public Data.CreditCard GetCreditCard(string cardNumber)
        {
            return _context.Values.Single(card => card.CardNumber.Equals(cardNumber));
        }

        public IEnumerable<Data.CreditCard> GetAllCreditCards()
        {
            return _context.Values;
        }

        public void UpdateCreditCard(string cardNumber, Data.CreditCard creditCard)
        {
            Data.CreditCard updatedCard = GetCreditCard(cardNumber);
            updatedCard.CardNumber = creditCard.CardNumber;
            updatedCard.CardType = creditCard.CardType;
            updatedCard.ExpMonth = creditCard.ExpMonth;
            updatedCard.ExpYear = creditCard.ExpYear;
            updatedCard.ModifiedDate = DateTime.UtcNow;
        }

        public void DeleteCreditCard(string cardNumber)
        {
            int cardId = GetCreditCard(cardNumber).CreditCardID;
            _context.Remove(cardId);
        }

        public void Dispose()
        {
            // unnecessary - in-memory implementation
        }
    }
}