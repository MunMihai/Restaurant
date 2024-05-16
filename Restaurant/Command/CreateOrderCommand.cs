using System;
namespace Restaurant.Command;

using MediatR;
using Restaurant.Controllers;
using Restaurant.Entities;

public class CreateOrderCommand : IRequest<decimal>
{
    public OrderTableDto Order { get; }

    public CreateOrderCommand(OrderTableDto order)
    {
        Order = order;
    }
}

