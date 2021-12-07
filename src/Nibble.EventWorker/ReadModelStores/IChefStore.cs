using Nibble.ReadModels;
using System;
using System.Threading.Tasks;

namespace Nibble.EventWorker
{
    public interface IChefStore
    {
        public Task<Chef> GetChef(Guid id);
        public Task AddChef(Chef chef);
        public Task AddMeal(Guid id, Meal meal);
        public Task RemoveMeal(Guid id, Guid mealId);
    }
}