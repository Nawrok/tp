using System.Collections.Generic;
using System.Linq;
using Logic;

namespace Model
{
    public class CreditCardService
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardService()
        {
            _creditCardService = new Logic.CreditCardService();
        }

        public CreditCardService(ICreditCardService service)
        {
            _creditCardService = service;
        }

        public void AddCreditCard(CreditCardModel creditCard)
        {
            _creditCardService.AddCreditCard(creditCard);
        }

        public CreditCardModel GetCreditCard(string cardNumber)
        {
            return _creditCardService.GetCreditCard(cardNumber) as CreditCardModel;
        }

        public IEnumerable<CreditCardModel> GetAllCreditCards()
        {
            return _creditCardService.GetAllCreditCards().Select(card => new CreditCardModel(card));
        }

        public void UpdateCreditCard(string cardNumber, CreditCardModel creditCard)
        {
            _creditCardService.UpdateCreditCard(cardNumber, creditCard);
        }

        public void DeleteCreditCard(string cardNumber)
        {
            _creditCardService.DeleteCreditCard(cardNumber);
        }
    }
}