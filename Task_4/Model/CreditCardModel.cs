﻿using Logic;

namespace Model
{
    public class CreditCardModel : ICreditCard
    {
        public CreditCardModel() { }

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
    }
}