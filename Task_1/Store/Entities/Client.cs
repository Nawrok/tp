using System;

namespace Store.Entities
{
    public class Client
    {
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
        public string Street { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return $"Street: {Street} | City: {City}";
        }
    }
}