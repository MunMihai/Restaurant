using System;
using Restaurant.Entities;
using Restaurant.FactoryMethod;

namespace Restaurant.Dtos
{
    public class MenuItemDto
    {
        public MenuItemType Type { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public decimal Price { get; set; }
        public bool ContainsNuts { get; set; }
        public string AllergenInfo { get; set; }
        public int SpiceLevel { get; set; }
    }

}
namespace Restaurant.Dto
{
    public class EventCreationDto
    {
        public EventType EventType { get; set; }
        public DateTime EventDateTime { get; set; }
        public int NumberOfPeople { get; set; }
        public List<int> VeganMenuItemIds { get; set; }
        public List<int> GlutenFreeMenuItemIds { get; set; }
        public List<int> SpicyMenuItemIds { get; set; }
    }
}
