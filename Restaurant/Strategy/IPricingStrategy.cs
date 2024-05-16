using System;
namespace Restaurant.Strategy
{
    // Strategy Interface
    public interface IPricingStrategy
    {
        decimal CalculatePrice(decimal basePrice);
    }

    // Concrete Strategy for Weekdays
    public class WeekdayPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(decimal basePrice)
        {
            // Apply weekday pricing logic here
            // For example, apply a discount for weekday orders
            return basePrice * 0.9m; // 10% discount
        }
    }

    // Concrete Strategy for Weekends
    public class WeekendPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(decimal basePrice)
        {
            // Apply weekend pricing logic here
            // For example, increase the price for weekend orders
            return basePrice * 1.1m; // 10% surcharge
        }
    }
}

