namespace Restaurant.Command;

using MediatR;
using Restaurant.Entities;

public class GetOrdersByPhoneNumberQuery : IRequest<List<OrderTable>>
{
    public string PhoneNumber { get; }

    public GetOrdersByPhoneNumberQuery(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
}

