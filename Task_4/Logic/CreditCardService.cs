using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Logic
{
    public class CreditCardService : ICreditCardService
    {
        private readonly SalesDataContext _context = new SalesDataContext();

        public void AddCreditCard(ICreditCard creditCard)
        {
            if (creditCard.ExpMonth <= 0 || creditCard.ExpMonth > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(creditCard.ExpMonth));
            }

            Data.CreditCard card = MapToDataCreditCard(creditCard);
            card.ModifiedDate = DateTime.UtcNow;
            _context.AddCreditCard(card);
        }

        public ICreditCard GetCreditCard(string cardNumber)
        {
            return new CreditCard(_context.GetCreditCard(cardNumber));
        }

        public IEnumerable<ICreditCard> GetAllCreditCards()
        {
            return _context.GetAllCreditCards().Select(card => new CreditCard(card));
        }

        public void UpdateCreditCard(string cardNumber, ICreditCard creditCard)
        {
            Data.CreditCard card = MapToDataCreditCard(creditCard);
            card.ModifiedDate = DateTime.UtcNow;
            _context.UpdateCreditCard(cardNumber, card);
        }

        public void DeleteCreditCard(string cardNumber)
        {
            _context.DeleteCreditCard(cardNumber);
        }

        private static Data.CreditCard MapToDataCreditCard(ICreditCard creditCard)
        {
            return new Data.CreditCard
            {
                CardNumber = creditCard.CardNumber,
                CardType = creditCard.CardType,
                ExpMonth = creditCard.ExpMonth,
                ExpYear = creditCard.ExpYear
            };
        }
    }
}