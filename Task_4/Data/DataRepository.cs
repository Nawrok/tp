using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Data
{
    public class DataRepository : SalesDataContext, IDataRepository
    {
        public void AddCreditCard(CreditCard creditCard)
        {
            CreditCard.InsertOnSubmit(creditCard);
            SubmitChanges();
        }

        public CreditCard GetCreditCard(int creditCardId)
        {
            return CreditCard.Single(card => card.CreditCardID == creditCardId);
        }

        public IEnumerable<CreditCard> GetAllCreditCards()
        {
            return CreditCard;
        }

        public void UpdateCreditCard(int creditCardId, CreditCard creditCard)
        {
            CreditCard updatedCard = GetCreditCard(creditCardId);
            updatedCard.CardNumber = creditCard.CardNumber;
            updatedCard.CardType = creditCard.CardType;
            updatedCard.ExpMonth = creditCard.ExpMonth;
            updatedCard.ExpYear = creditCard.ExpYear;
            updatedCard.ModifiedDate = DateTime.Today;
            SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public void DeleteCreditCard(int creditCardId)
        {
            CreditCard creditCard = GetCreditCard(creditCardId);
            CreditCard.DeleteOnSubmit(creditCard);
            SubmitChanges(ConflictMode.ContinueOnConflict);
        }
    }
}