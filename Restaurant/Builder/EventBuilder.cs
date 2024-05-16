using Restaurant.DB;
using Restaurant.Entities;
using System;
using System.Collections.Generic;

namespace Restaurant.Builder
{
    public class EventBuilder
    {
        private readonly RestaurantDbContext _context;
        private readonly EventOrder _eventOrder;

        public EventBuilder(RestaurantDbContext context)
        {
            _context = context;
            _eventOrder = new EventOrder();
        }

        public EventBuilder SetEventType(EventType eventType)
        {
            _eventOrder.EventType = eventType;
            return this;
        }

        public EventBuilder SetEventDateTime(DateTime eventDateTime)
        {
            _eventOrder.EventDateTime = eventDateTime;
            return this;
        }

        public EventBuilder SetNumberOfPeople(int numberOfPeople)
        {
            _eventOrder.NumberOfPeople = numberOfPeople;
            return this;
        }

        public EventBuilder AddVeganMenuItems(List<int> veganMenuItemIds)
        {
            foreach (var id in veganMenuItemIds)
            {
                var menuItem = _context.VeganMenuItems.Find(id);
                if (menuItem != null)
                {
                    _eventOrder.VeganMenuItems.Add(menuItem);
                }
            }
            return this;
        }
     

        public EventBuilder AddGlutenFreeMenuItems(List<int> glutenFreeMenuItemIds)
        {
            foreach (var id in glutenFreeMenuItemIds)
            {
                var menuItem = _context.GlutenFreeMenuItems.Find(id);
                if (menuItem != null)
                {
                    _eventOrder.GlutenFreeMenuItems.Add(menuItem);
                }
            }
            return this;
        }

        public EventBuilder AddSpicyMenuItems(List<int> spicyMenuItemIds)
        {
            foreach (var id in spicyMenuItemIds)
            {
                var menuItem = _context.SpicyMenuItems.Find(id);
                if (menuItem != null)
                {
                    _eventOrder.SpicyMenuItems.Add(menuItem);
                }
            }
            return this;
        }


        public EventOrder Build()
        {
            return _eventOrder;
        }
    }
}
