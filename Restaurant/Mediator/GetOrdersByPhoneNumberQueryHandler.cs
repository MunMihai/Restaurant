using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Command;
using Restaurant.DB;
using Restaurant.Entities;
namespace Restaurant.Mediator;

public class GetOrdersByPhoneNumberQueryHandler : IRequestHandler<GetOrdersByPhoneNumberQuery, List<OrderTable>>
{
    private readonly RestaurantDbContext _context;

    public GetOrdersByPhoneNumberQueryHandler(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderTable>> Handle(GetOrdersByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var ordersByPhoneNumber = await _context.Orders.Where(o => o.ClientPhoneNumber == request.PhoneNumber).ToListAsync();
        return ordersByPhoneNumber;
    }
}

