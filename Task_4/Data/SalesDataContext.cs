using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Data
{
    public partial class SalesDataContext
    {
        public void AddCreditCard(CreditCard creditCard)
        {
            CreditCard.InsertOnSubmit(creditCard);
            SubmitChanges();
        }

        public CreditCard GetCreditCard(string cardNumber)
        {
            return CreditCard.Single(card => card.CardNumber.Equals(cardNumber));
        }

        public IEnumerable<CreditCard> GetAllCreditCards()
        {
            return CreditCard;
        }

        public void UpdateCreditCard(string cardNumber, CreditCard creditCard)
        {
            CreditCard updatedCard = GetCreditCard(cardNumber);
            updatedCard.CardNumber = creditCard.CardNumber;
            updatedCard.CardType = creditCard.CardType;
            updatedCard.ExpMonth = creditCard.ExpMonth;
            updatedCard.ExpYear = creditCard.ExpYear;
            updatedCard.ModifiedDate = DateTime.UtcNow;
            SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public void DeleteCreditCard(string cardNumber)
        {
            CreditCard creditCard = GetCreditCard(cardNumber);
            CreditCard.DeleteOnSubmit(creditCard);
            SubmitChanges(ConflictMode.ContinueOnConflict);
        }
    }
}