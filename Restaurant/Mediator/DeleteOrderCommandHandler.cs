using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Command;
using Restaurant.DB;
namespace Restaurant.Mediator;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly RestaurantDbContext _context;

    public DeleteOrderCommandHandler(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToRemove = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId);
        if (orderToRemove == null)
            return false;

        _context.Orders.Remove(orderToRemove);
        await _context.SaveChangesAsync();
        return true;
    }
}

