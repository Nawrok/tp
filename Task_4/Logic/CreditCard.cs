namespace Logic
{
    internal class CreditCard : ICreditCard
    {
        public CreditCard() { }

        public CreditCard(Data.CreditCard creditCard)
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