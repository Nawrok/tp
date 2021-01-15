using System;
using System.Collections.Generic;
using Data;

namespace Logic
{
    public class DataService
    {
        private readonly IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public void AddCreditCard(CreditCard creditCard)
        {
            using (_dataRepository)
            {
                _dataRepository.AddCreditCard(creditCard);
            }
        }

        public CreditCard GetCreditCard(int creditCardId)
        {
            using (_dataRepository)
            {
                return _dataRepository.GetCreditCard(creditCardId);
            }
        }

        public IEnumerable<CreditCard> GetAllCreditCards()
        {
            using (_dataRepository)
            {
                return _dataRepository.GetAllCreditCards();
            }
        }

        public void UpdateCreditCard(int creditCardId, CreditCard creditCard)
        {
            using (_dataRepository)
            {
                _dataRepository.UpdateCreditCard(creditCardId, creditCard);
            }
        }

        public void DeleteCreditCard(int creditCardId)
        {
            using (_dataRepository)
            {
                _dataRepository.DeleteCreditCard(creditCardId);
            }
        }
    }
}