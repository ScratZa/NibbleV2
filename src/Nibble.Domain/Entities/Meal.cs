using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Domain.Entities
{
    public class Meal : IEntity
    {
        public Meal(Guid id,string name, string description, string[] tags, string[] ingredients, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Tags = tags;
            Ingredients = ingredients;
            Price = price;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public string[] Ingredients { get; set; }
        public decimal Price { get; set; }
    }
}
