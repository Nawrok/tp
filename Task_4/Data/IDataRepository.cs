using System.Collections.Generic;

namespace Data
{
    public interface IDataRepository
    {
        void AddCreditCard(CreditCard creditCard);

        CreditCard GetCreditCard(int creditCardId);

        IEnumerable<CreditCard> GetAllCreditCards();

        void UpdateCreditCard(int creditCardId, CreditCard creditCard);

        void DeleteCreditCard(int creditCardId);
    }
}