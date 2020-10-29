namespace Store.Entities
{
    public class Client
    {
        public Client() { }

        public Client(string name, string surname, string email, Address address)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Address = address;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} | Surname {Surname} | Email {Email} | Address: {Address}";
        }
    }

    public class Address
    {
        public Address() { }

        public Address(string street, string city)
        {
            Street = street;
            City = city;
        }

        public string Street { get; set; }
        public string City { get; set; }
       
        public override string ToString()
        {
            return $"Street: {Street} | City: {City}";
        }
    }
}