namespace Store.Entities
{
    public class Client
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public Client(string name, string surname, string email, Address address)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Address = address;
        }

        public override string ToString()
        {
            return $"Name: {Name} | Surname {Surname} | Email {Email} | Address: {Address}";
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }

        public Address(string street, string city)
        {
            Street = street;
            City = city;
        }

        public override string ToString()
        {
            return $"Street: {Street} | City: {City}";
        }
    }
}