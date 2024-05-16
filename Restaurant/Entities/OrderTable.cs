using System;
using Restaurant.Strategy;

namespace Restaurant.Entities
{
    // Context Class (OrderTable)
    public class OrderTable
    {
        private readonly IPricingStrategy _pricingStrategy;
        public static decimal _basePrice = 12; // Static base price

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public string ClientPhoneNumber { get; set; } // New property for client's phone number

        public OrderTable(DateTime orderDate, string clientPhoneNumber)
        {
            OrderDate = orderDate;
            ClientPhoneNumber = clientPhoneNumber; // Initialize the client phone number

            // Determine the pricing strategy based on the order date
            _pricingStrategy = IsWeekend(orderDate)
                ? (IPricingStrategy)new WeekendPricingStrategy()
                : new WeekdayPricingStrategy();

            // Calculate the total price using the selected pricing strategy
            Price = CalculateTotalPrice();
        }

        public decimal CalculateTotalPrice()
        {
            // Calculate the total price using the selected pricing strategy
            return _pricingStrategy.CalculatePrice(_basePrice);
        }

        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
