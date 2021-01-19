using System.Collections.Generic;
using System.Linq;

namespace Logic.Tests.Instrumentation
{
    public class InMemoryCreditCardService : ICreditCardService
    {
        private readonly List<ICreditCard> _context = new List<ICreditCard>();

        public InMemoryCreditCardService()
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


        public void AddCreditCard(ICreditCard creditCard)
        {
            _context.Add(creditCard);
        }

        public ICreditCard GetCreditCard(string cardNumber)
        {
            return _context.Single(card => card.CardNumber.Equals(cardNumber));
        }

        public IEnumerable<ICreditCard> GetAllCreditCards()
        {
            return _context;
        }

        public void UpdateCreditCard(string cardNumber, ICreditCard creditCard)
        {
            ICreditCard updatedCard = GetCreditCard(cardNumber);
            updatedCard.CardNumber = creditCard.CardNumber;
            updatedCard.CardType = creditCard.CardType;
            updatedCard.ExpMonth = creditCard.ExpMonth;
            updatedCard.ExpYear = creditCard.ExpYear;
        }

        public void DeleteCreditCard(string cardNumber)
        {
            ICreditCard card = GetCreditCard(cardNumber);
            _context.Remove(card);
        }
    }
}