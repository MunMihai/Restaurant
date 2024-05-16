using Restaurant.FactoryMethod;

namespace Restaurant.Repositories;

public interface IPreorderedDishRepository
{
    Task<bool> AddPreorderedDish(int tableOrderId, int dishId, MenuItemType menuItemType);
    Task<int?> ClonePreorderedDish(int preorderedDishId);

}
//facade pattern