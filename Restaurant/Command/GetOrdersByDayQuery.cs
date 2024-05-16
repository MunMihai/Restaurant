namespace Restaurant.Command;

using MediatR;
using Restaurant.Entities;

public class GetOrdersByDayQuery : IRequest<List<OrderTable>>
{
    public DateTime Date { get; }

    public GetOrdersByDayQuery(DateTime date)
    {
        Date = date;
    }
}

