using Microsoft.AspNetCore.Mvc;
using Restaurant.Builder;
using Restaurant.DB;
using Restaurant.Dto;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public EventController(RestaurantDbContext restaurantDbContext)
        {
            _context = restaurantDbContext;
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] EventCreationDto eventData)
        {
            var eventBuilder = new EventBuilder(_context); 

            var eventOrder = eventBuilder
                .SetEventType(eventData.EventType)
                .SetEventDateTime(eventData.EventDateTime)
                .SetNumberOfPeople(eventData.NumberOfPeople)
                .AddVeganMenuItems(eventData.VeganMenuItemIds)
                .AddGlutenFreeMenuItems(eventData.GlutenFreeMenuItemIds)
                .AddSpicyMenuItems(eventData.SpicyMenuItemIds)
                .Build();

            // Perform any additional logic here, such as validation or error handling

            // Save the eventOrder to the database
            _context.EventOrders.Add(eventOrder);
            _context.SaveChanges();

            return Ok("Event created successfully");
        }
    }
}
