using System;
using System.Collections.Generic;

namespace Data
{
    public interface IDataRepository : IDisposable
    {
        void AddCreditCard(CreditCard creditCard);

        CreditCard GetCreditCard(string cardNumber);

        IEnumerable<CreditCard> GetAllCreditCards();

        void UpdateCreditCard(string cardNumber, CreditCard creditCard);

        void DeleteCreditCard(string cardNumber);
    }
}