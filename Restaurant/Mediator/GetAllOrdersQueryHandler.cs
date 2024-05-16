using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Command;
using Restaurant.DB;
using Restaurant.Entities;
namespace Restaurant.Mediator;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderTable>>
{
    private readonly RestaurantDbContext _context;

    public GetAllOrdersQueryHandler(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderTable>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var allOrders = await _context.Orders.ToListAsync();
        return allOrders;
    }
}

