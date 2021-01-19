using Logic;

namespace Model
{
    public class CreditCardModel : ICreditCard
    {
        public CreditCardModel()
        {
            CardNumber = "";
            CardType = "";
        }

        public CreditCardModel(ICreditCard creditCard)
        {
            CardNumber = creditCard.CardNumber;
            CardType = creditCard.CardType;
            ExpMonth = creditCard.ExpMonth;
            ExpYear = creditCard.ExpYear;
        }

        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }

        public CreditCardModel Clone()
        {
            return new CreditCardModel
            {
                CardNumber = CardNumber,
                CardType = CardType,
                ExpMonth = ExpMonth,
                ExpYear = ExpYear
            };
        }
    }
}