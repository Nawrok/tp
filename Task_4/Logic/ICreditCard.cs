namespace Logic
{
    public interface ICreditCard
    {
        string CardNumber { get; set; }
        string CardType { get; set; }
        byte ExpMonth { get; set; }
        short ExpYear { get; set; }
    }
}