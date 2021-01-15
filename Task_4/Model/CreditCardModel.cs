using Logic;

namespace Model
{
    public class CreditCardModel : ICreditCard
    {
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }
    }
}