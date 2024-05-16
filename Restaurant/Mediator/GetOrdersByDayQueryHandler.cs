using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Command;
using Restaurant.DB;
using Restaurant.Entities;
namespace Restaurant.Mediator;

public class GetOrdersByDayQueryHandler : IRequestHandler<GetOrdersByDayQuery, List<OrderTable>>
{
    private readonly RestaurantDbContext _context;

    public GetOrdersByDayQueryHandler(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderTable>> Handle(GetOrdersByDayQuery request, CancellationToken cancellationToken)
    {
        var ordersByDay = await _context.Orders.Where(o => o.OrderDate.Date == request.Date.Date).ToListAsync();
        return ordersByDay;
    }
}

