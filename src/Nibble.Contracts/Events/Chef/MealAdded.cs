using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Contracts.Events.Chef
{
    public class MealAdded : IEvent
    {
        public Guid Id { get; set; }
        //better way to do this
        public Guid ChefId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public string[] Ingredients { get; set; }
        public decimal Price { get; set; }
        public MealAdded(Guid id, Guid chefId, string name, string description, string[] tags, string[] ingredients, decimal price)
        {
            Id = id;
            ChefId = chefId;
            Name = name;
            Description = description;
            Tags = tags;
            Ingredients = ingredients;
            Price = price;
        }
        public int CompareTo(object obj)
        {
            if (obj is MealAdded added &&
                   Id.Equals(added.Id) &&
                   Name == added.Name &&
                   Description == added.Description &&
                   EqualityComparer<string[]>.Default.Equals(Tags, added.Tags) &&
                   EqualityComparer<string[]>.Default.Equals(Ingredients, added.Ingredients) &&
                   Price == added.Price)
                return 0;

            return -1;
        }
    }
}
