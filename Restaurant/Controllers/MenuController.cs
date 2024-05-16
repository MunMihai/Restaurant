using Microsoft.AspNetCore.Mvc;
using Restaurant.DB;
using Restaurant.Dtos;
using Restaurant.Entities;
using Restaurant.FactoryMethod;

namespace Restaurant.Controllers;
[Route("api/menu")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly RestaurantDbContext _context;
    private readonly MenuItemFactory _menuItemFactory;

    public MenuController(RestaurantDbContext context, MenuItemFactory menuItemFactory)
    {
        _context = context;
        _menuItemFactory = menuItemFactory;
    }
    [HttpPost("vegan/create")]
    public IActionResult CreateVeganMenuItem([FromBody] MenuItemDto menuItemDto)
    {
        try
        {
            IMenuItem menuItem = _menuItemFactory.CreateMenuItem(MenuItemType.Vegan, menuItemDto.Name, menuItemDto.Ingredients, menuItemDto.Price, menuItemDto.ContainsNuts, "", 0);
            _context.VeganMenuItems.Add((VeganMenuItem)menuItem);
            _context.SaveChanges();
            return Ok("Vegan menu item created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create vegan menu item: {ex.Message}");
        }
    }

    [HttpPost("spicy/create")]
    public IActionResult CreateSpicyMenuItem([FromBody] MenuItemDto menuItemDto)
    {
        try
        {
            IMenuItem menuItem = _menuItemFactory.CreateMenuItem(MenuItemType.Spicy, menuItemDto.Name, menuItemDto.Ingredients, menuItemDto.Price, false, "", menuItemDto.SpiceLevel);
            _context.SpicyMenuItems.Add((SpicyMenuItem)menuItem);
            _context.SaveChanges();
            return Ok("Spicy menu item created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create spicy menu item: {ex.Message}");
        }
    }

    [HttpPost("glutenfree/create")]
    public IActionResult CreateGlutenFreeMenuItem([FromBody] MenuItemDto menuItemDto)
    {
        try
        {
            IMenuItem menuItem = _menuItemFactory.CreateMenuItem(MenuItemType.GlutenFree, menuItemDto.Name, menuItemDto.Ingredients, menuItemDto.Price, false, menuItemDto.AllergenInfo, 0);
            _context.GlutenFreeMenuItems.Add((GlutenFreeMenuItem)menuItem);
            _context.SaveChanges();
            return Ok("Gluten-free menu item created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create gluten-free menu item: {ex.Message}");
        }
    }
    // GET: api/menu
    [HttpGet]
    public IActionResult GetAllMenuItems()
    {
        try
        {
            var veganMenuItems = _context.VeganMenuItems.ToList();
            var spicyMenuItems = _context.SpicyMenuItems.ToList();
            var glutenFreeMenuItems = _context.GlutenFreeMenuItems.ToList();

            var allMenuItems = new List<IMenuItem>();
            allMenuItems.AddRange(veganMenuItems);
            allMenuItems.AddRange(spicyMenuItems);
            allMenuItems.AddRange(glutenFreeMenuItems);

            return Ok(allMenuItems);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get all menu items: {ex.Message}");
        }
    }


    // GET: api/menu?type=vegan&id=5
    [HttpGet("find")]
    public IActionResult FindMenuItem(string type, int id)
    {
        switch (type.ToLower())
        {
            case "vegan":
                var veganMenuItem = _context.VeganMenuItems.Find(id);
                if (veganMenuItem == null)
                {
                    return NotFound();
                }
                return Ok(veganMenuItem);
            case "spicy":
                var spicyMenuItem = _context.SpicyMenuItems.Find(id);
                if (spicyMenuItem == null)
                {
                    return NotFound();
                }
                return Ok(spicyMenuItem);
            case "glutenfree":
                var glutenFreeMenuItem = _context.GlutenFreeMenuItems.Find(id);
                if (glutenFreeMenuItem == null)
                {
                    return NotFound();
                }
                return Ok(glutenFreeMenuItem);
            default:
                return BadRequest("Invalid menu item type");
        }
    }
}
