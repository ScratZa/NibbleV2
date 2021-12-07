using Nibble.Contracts.Events.Chef;
using Nibble.Domain.Entities;
using Nibble.Domain.ValueObjects;
using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Domain.Aggregates
{
    public class ChefAggregate : BaseAggregate
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string CookingStyle { get; private set; }
        public Address PickupAddress { get; private set; }
        public List<Meal> Meals { get; private set; }
        internal static IAggregate Create(Guid id, string firstName, string lastName, string cookingStyle, string addressName, double lat, double lon)
        {
            return new ChefAggregate(id, firstName, lastName, cookingStyle, addressName, lat, lon);
        }

        public ChefAggregate()
        {
            AddEventHandler<ChefCreated>(ApplyCreateChef);
            AddEventHandler<MealAdded>(ApplyMealAdded);
            AddEventHandler<MealRemoved>(ApplyMealRemoved);
        }

        public IAggregate AddMeal(string name, string description, decimal price, string[] tags, string[] ingrediants)
        {
            if (!this.Meals.Select(x => x.Name).Contains(name))
                RaiseEvent(new MealAdded(Guid.NewGuid(),Id,name,description,tags,ingrediants,price));

            return this;
        }

        public IAggregate RemoveMeal(Guid id)
        {
            RaiseEvent(new MealRemoved(id,Id));
            return this;
        }

        private ChefAggregate(Guid id, string firstName, string lastName, string cookingStyle, string addressName, double lat, double lon) : this()
        {
            RaiseEvent(new ChefCreated(id, firstName, lastName, cookingStyle, addressName, lat, lon));
        }

        private void ApplyCreateChef(ChefCreated @event)
        {
            Id = @event.Id;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            PickupAddress = new Address
            {
                FriendlyName = "Default",
                AddressName = @event.AddressName,
                Latitude = @event.Latitude,
                Longitude = @event.Longitude,
            };
            CookingStyle = @event.CookingStyle;
            Meals = new List<Meal>();
        }

        private void ApplyMealAdded(MealAdded @event)
        {
            var meal = new Meal(@event.Id, @event.Name, @event.Description, @event.Tags, @event.Ingredients, @event.Price);
            Meals.Add(meal);
        }

        private void ApplyMealRemoved(MealRemoved @event)
        {
            Meals.RemoveAll(x=> x.Id == @event.Id);
        }
    }
}
