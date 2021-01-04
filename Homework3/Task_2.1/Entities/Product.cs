using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_2._1.Entities
{
    public class Product
    {
        public string Id { get; }

        public string Brand { get; }

        public string Model { get; }

        public decimal Price { get; }

        public List<Tag> Tags { get; set; }

        public Product(string id, string brand, string model, decimal price)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Price = price;
        }

        public override string ToString()
        {
            return $"#{Id} {Brand} - {Model} - ${Price} [{string.Join(", ", Tags.Select(x => x.TagValue))}]";
        }
    }
}
