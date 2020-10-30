using System;

namespace Store.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public Product(Guid id, string name, string description, string type)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
        }

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | Description: {Description} | Type: {Type}";
        }
    }
}
