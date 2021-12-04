using Nibble.ReadModels;
using System.Collections.Generic;

namespace Nibble.Mappers
{
    public static class ChefMappers
    {
        public static IEnumerable<ChefListItem> ToChefListItems(this IEnumerable<Chef> chefs)
        {
            foreach(var chef in chefs)
            {
                yield return  chef.ToChefListItem();
            }
        }
        public static ChefListItem ToChefListItem(this Chef chef)
        {
            return new ChefListItem
            {
                Name = chef.FirstName + " " + chef.LastName,
                Id = chef.Id,
                CookingStyle = chef.CookingStyle,
            };
        }
    }
}