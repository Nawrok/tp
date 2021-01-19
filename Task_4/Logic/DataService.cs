using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Logic
{
    public class DataService : IDataService
    {
        private readonly IDataRepository _dataRepository;

        public DataService() : this(new DataRepository()) { }

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public void AddCreditCard(ICreditCard creditCard)
        {
            if (creditCard.ExpMonth <= 0 || creditCard.ExpMonth > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(creditCard.ExpMonth));
            }

            using (_dataRepository)
            {
                Data.CreditCard card = MapToDataCreditCard(creditCard);
                card.ModifiedDate = DateTime.UtcNow;
                _dataRepository.AddCreditCard(card);
            }
        }

        public ICreditCard GetCreditCard(string cardNumber)
        {
            using (_dataRepository)
            {
                return new CreditCard(_dataRepository.GetCreditCard(cardNumber));
            }
        }

        public IEnumerable<ICreditCard> GetAllCreditCards()
        {
            using (_dataRepository)
            {
                return _dataRepository.GetAllCreditCards().Select(card => new CreditCard(card));
            }
        }

        public void UpdateCreditCard(string cardNumber, ICreditCard creditCard)
        {
            using (_dataRepository)
            {
                Data.CreditCard card = MapToDataCreditCard(creditCard);
                card.ModifiedDate = DateTime.UtcNow;
                _dataRepository.UpdateCreditCard(cardNumber, card);
            }
        }

        public void DeleteCreditCard(string cardNumber)
        {
            using (_dataRepository)
            {
                _dataRepository.DeleteCreditCard(cardNumber);
            }
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