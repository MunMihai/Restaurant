namespace Restaurant.Command;

using MediatR;
using Restaurant.Entities;

public class GetAllOrdersQuery : IRequest<List<OrderTable>>
{
}

