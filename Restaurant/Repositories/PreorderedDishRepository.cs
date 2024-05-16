using Microsoft.EntityFrameworkCore;
using Restaurant.DB;
using Restaurant.Entities;
using Restaurant.FactoryMethod;

namespace Restaurant.Repositories;

public class PreorderedDishRepository : IPreorderedDishRepository
{
    private readonly RestaurantDbContext _context;

    public PreorderedDishRepository(RestaurantDbContext context)
    {
        _context = context;
    }
    public async Task<int?> ClonePreorderedDish(int preorderedDishId)
    {
        // Retrieve the preordered dish by its ID
        var preorderedDish = await _context.PreorderedDishes.FindAsync(preorderedDishId);
        if (preorderedDish == null)
        {
            return null;
        }

        // Detach the original preordered dish from the context
        _context.Entry(preorderedDish).State = EntityState.Detached;

        // Clone the preordered dish using the Prototype pattern
        var clonedDish = preorderedDish.Clone();

        // Set the ID of the cloned preordered dish to be original ID plus 1000
        clonedDish.Id += 1000;

        // Add the cloned preordered dish to the database
        _context.PreorderedDishes.Add(clonedDish);
        await _context.SaveChangesAsync();

        // Return the ID of the cloned preordered dish
        return clonedDish.Id;
    }

    public async Task<bool> AddPreorderedDish(int tableOrderId, int dishId, MenuItemType menuItemType)
    {
        
            switch (menuItemType)
            {
                case MenuItemType.Vegan:
                    var veganDish = await _context.VeganMenuItems.FindAsync(dishId);
                    if (veganDish == null)
                        return false;

                    await AddDishToPreorderedList(tableOrderId, veganDish);
                    break;
                case MenuItemType.GlutenFree:
                    var glutenFreeDish = await _context.GlutenFreeMenuItems.FindAsync(dishId);
                    if (glutenFreeDish == null)
                        return false;

                    await AddDishToPreorderedList(tableOrderId, glutenFreeDish);
                    break;
                case MenuItemType.Spicy:
                    var spicyDish = await _context.SpicyMenuItems.FindAsync(dishId);
                    if (spicyDish == null)
                        return false;

                    await AddDishToPreorderedList(tableOrderId, spicyDish);
                    break;
                default:
                    return false;
            }

            return true;
        
        
    }

    private async Task AddDishToPreorderedList(int tableOrderId, IMenuItem dish)
    {
        var preorderedDish = await _context.PreorderedDishes
            .FirstOrDefaultAsync(pd => pd.TableOrderId == tableOrderId);

        if (preorderedDish == null)
        {
            preorderedDish = new PreorderedDish { TableOrderId = tableOrderId };
            _context.PreorderedDishes.Add(preorderedDish);
        }

        switch (dish)
        {
            case VeganMenuItem vegan:
                preorderedDish.VeganDishes.Add(vegan);
                break;
            case GlutenFreeMenuItem glutenFree:
                preorderedDish.GlutenFreeDishes.Add(glutenFree);
                break;
            case SpicyMenuItem spicy:
                preorderedDish.SpicyDishes.Add(spicy);
                break;
            default:
                throw new ArgumentException("Invalid dish type");
        }

        await _context.SaveChangesAsync();
    }
}
//facade pattern