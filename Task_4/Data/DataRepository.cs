using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Data
{
    public class DataRepository : IDataRepository
    {
        private readonly SalesDataContext _context = new SalesDataContext();

        public void AddCreditCard(CreditCard creditCard)
        {
            _context.CreditCard.InsertOnSubmit(creditCard);
            _context.SubmitChanges();
        }

        public CreditCard GetCreditCard(string cardNumber)
        {
            return _context.CreditCard.Single(card => card.CardNumber.Equals(cardNumber));
        }

        public IEnumerable<CreditCard> GetAllCreditCards()
        {
            return _context.CreditCard;
        }

        public void UpdateCreditCard(string cardNumber, CreditCard creditCard)
        {
            CreditCard updatedCard = GetCreditCard(cardNumber);
            updatedCard.CardNumber = creditCard.CardNumber;
            updatedCard.CardType = creditCard.CardType;
            updatedCard.ExpMonth = creditCard.ExpMonth;
            updatedCard.ExpYear = creditCard.ExpYear;
            updatedCard.ModifiedDate = DateTime.Today;
            _context.SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public void DeleteCreditCard(string cardNumber)
        {
            CreditCard creditCard = GetCreditCard(cardNumber);
            _context.CreditCard.DeleteOnSubmit(creditCard);
            _context.SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}