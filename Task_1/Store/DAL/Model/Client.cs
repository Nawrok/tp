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

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} | Surname {Surname} | Email {Email} | City: {City}";
        }
    }
}