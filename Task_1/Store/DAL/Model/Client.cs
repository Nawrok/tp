namespace Store.DAL.Model
{
    public class Client
    {
        public Client(string name, string surname, string email, string city)
        {
            Name = name;
            Surname = surname;
            Email = email;
            City = city;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public string City { get; }

        public override string ToString()
        {
            return $"Name: {Name} | Surname {Surname} | Email {Email} | City: {City}";
        }
    }
}