using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Restaurant.Command;
using Restaurant.DB;
using Restaurant.Entities;
using Restaurant.Strategy;

namespace Restaurant.Mediator
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, decimal>
    {
        private readonly RestaurantDbContext _context;

        public CreateOrderCommandHandler(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.Order == null)
                throw new ArgumentNullException(nameof(request.Order));

            // Determine the pricing strategy based on the order date
            IPricingStrategy pricingStrategy = IsWeekend(request.Order.OrderDate)
                ? (IPricingStrategy)new WeekendPricingStrategy()
                : new WeekdayPricingStrategy();

            decimal totalPrice = pricingStrategy.CalculatePrice(OrderTable._basePrice); // Assuming _basePrice is a static field
            var order = new OrderTable(
                request.Order.OrderDate, // Provide the order date
                request.Order.ClientPhoneNumber // Provide the client phone number
            )
            {
                Price = totalPrice
            };
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return totalPrice;
        }

        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
