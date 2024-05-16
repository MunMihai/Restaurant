using Microsoft.AspNetCore.Mvc;
using Restaurant.FactoryMethod;
using Restaurant.Repositories;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreorderedDishController : ControllerBase
    {
        private readonly IPreorderedDishRepository _preorderedDishRepository;

        public PreorderedDishController(IPreorderedDishRepository preorderedDishRepository)
        {
            _preorderedDishRepository = preorderedDishRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPreorderedDish(int tableOrderId, int dishId, MenuItemType dishType)
        {
            var success = await _preorderedDishRepository.AddPreorderedDish(tableOrderId, dishId, dishType);
            if (success)
            {
                return Ok("Dish preordered successfully.");
            }
            return BadRequest("Failed to preorder dish.");
        }

        [HttpPost("clone")]
        public async Task<IActionResult> ClonePreorderedDish(int preorderedDishId)
        {
            var clonedDishId = await _preorderedDishRepository.ClonePreorderedDish(preorderedDishId);
            if (clonedDishId != null)
            {
                return Ok($"Preordered dish cloned successfully with new ID: {clonedDishId}");
            }
            return BadRequest("Failed to clone preordered dish.");
        }
    }
}
