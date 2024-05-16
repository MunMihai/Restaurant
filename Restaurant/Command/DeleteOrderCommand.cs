namespace Restaurant.Command;

using MediatR;

public class DeleteOrderCommand : IRequest<bool>
{
    public int OrderId { get; }

    public DeleteOrderCommand(int orderId)
    {
        OrderId = orderId;
    }
}

