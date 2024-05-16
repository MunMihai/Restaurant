using System;
using Restaurant.Entities;

namespace Restaurant.FactoryMethod
{
    public class MenuItemFactory
    {
        public IMenuItem CreateMenuItem(MenuItemType type, string name, List<string> ingredients, decimal price, bool containsNuts = false, string allergenInfo = "", int spiceLevel = 0)
        {
            switch (type)
            {
                case MenuItemType.Vegan:
                    return new VeganMenuItem { Name = name, Ingredients = ingredients, Price = price, ContainsNuts = containsNuts };
                case MenuItemType.GlutenFree:
                    return new GlutenFreeMenuItem { Name = name, Ingredients = ingredients, Price = price, AllergenInfo = allergenInfo };
                case MenuItemType.Spicy:
                    return new SpicyMenuItem { Name = name, Ingredients = ingredients, Price = price, SpiceLevel = spiceLevel };
                default:
                    throw new ArgumentException("Invalid menu item type");
            }
        }
    }

}

