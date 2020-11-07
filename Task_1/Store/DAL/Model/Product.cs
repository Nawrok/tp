using System;

namespace Store.DAL.Model
{
    public class Product
    {
        public Product(Guid id, string name, string description, string type)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Type { get; }

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | Description: {Description} | Type: {Type}";
        }
    }
}