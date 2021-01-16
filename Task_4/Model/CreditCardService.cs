using System.Collections.Generic;
using System.Linq;
using Logic;

namespace Model
{
    public class CreditCardService
    {
        private readonly DataService _dataService;

        public CreditCardService()
        {
            _dataService = new DataService();
        }

        public CreditCardService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void AddCreditCard(CreditCardModel creditCard)
        {
            _dataService.AddCreditCard(creditCard);
        }

        public CreditCardModel GetCreditCard(string cardNumber)
        {
            return _dataService.GetCreditCard(cardNumber) as CreditCardModel;
        }

        public IEnumerable<CreditCardModel> GetAllCreditCards()
        {
            return _dataService.GetAllCreditCards().Cast<CreditCardModel>();
        }

        public void UpdateCreditCard(string cardNumber, CreditCardModel creditCard)
        {
            _dataService.UpdateCreditCard(cardNumber, creditCard);
        }

        public void DeleteCreditCard(string cardNumber)
        {
            _dataService.DeleteCreditCard(cardNumber);
        }
    }
}