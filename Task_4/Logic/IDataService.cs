using System.Collections.Generic;

namespace Logic
{
    public interface IDataService
    {
        void AddCreditCard(ICreditCard creditCard);
        ICreditCard GetCreditCard(string cardNumber);
        IEnumerable<ICreditCard> GetAllCreditCards();
        void UpdateCreditCard(string cardNumber, ICreditCard creditCard);
        void DeleteCreditCard(string cardNumber);
    }
}