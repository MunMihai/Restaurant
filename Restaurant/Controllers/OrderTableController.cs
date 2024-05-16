using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Command;
using Restaurant.Entities;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTableController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderTableController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/OrderTable
        [HttpPost("order")]
        public async Task<IActionResult> PostOrder([FromBody] OrderTableDto order)
        {
            var price = await _mediator.Send(new CreateOrderCommand(order));

                return Ok(price);
        }

        // GET: api/OrderTable/date/{date}
        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetOrdersByDay(DateTime date)
        {
            var orders = await _mediator.Send(new GetOrdersByDayQuery(date));
            return Ok(orders);
        }

        // GET: api/OrderTable/phone/{phoneNumber}
        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetOrdersByPhoneNumber(string phoneNumber)
        {
            var orders = await _mediator.Send(new GetOrdersByPhoneNumberQuery(phoneNumber));
            return Ok(orders);
        }

        // GET: api/OrderTable
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(orders);
        }

        // DELETE: api/OrderTable/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));

            if (result)
                return Ok($"Order with ID {id} deleted successfully.");
            else
                return NotFound($"Order with ID {id} not found.");
        }
    }

    public class OrderTableDto
    {
        public DateTime OrderDate { get; set; }
        public string ClientPhoneNumber { get; set; } 
    }
}
