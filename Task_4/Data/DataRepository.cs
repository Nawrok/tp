using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Data
{
    public class DataRepository : IDataRepository, IDisposable
    {
        private readonly SalesDataContext _context = new SalesDataContext();

        public void AddCreditCard(CreditCard creditCard)
        {
            _context.CreditCard.InsertOnSubmit(creditCard);
            _context.SubmitChanges();
        }

        public CreditCard GetCreditCard(int creditCardId)
        {
            return _context.CreditCard.Single(card => card.CreditCardID == creditCardId);
        }

        public IEnumerable<CreditCard> GetAllCreditCards()
        {
            return _context.CreditCard;
        }

        public void UpdateCreditCard(int creditCardId, CreditCard creditCard)
        {
            CreditCard updatedCard = GetCreditCard(creditCardId);
            updatedCard.CardNumber = creditCard.CardNumber;
            updatedCard.CardType = creditCard.CardType;
            updatedCard.ExpMonth = creditCard.ExpMonth;
            updatedCard.ExpYear = creditCard.ExpYear;
            updatedCard.ModifiedDate = DateTime.Today;
            _context.SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public void DeleteCreditCard(int creditCardId)
        {
            CreditCard creditCard = GetCreditCard(creditCardId);
            _context.CreditCard.DeleteOnSubmit(creditCard);
            _context.SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}