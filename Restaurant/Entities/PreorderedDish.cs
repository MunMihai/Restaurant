using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Entities;

public class PreorderedDish
{
    public int Id { get; set; }

    [ForeignKey("OrderTable")]
    public int TableOrderId { get; set; }
    public OrderTable TableOrder { get; set; }

    public ICollection<VeganMenuItem> VeganDishes { get; set; } // Collection of VeganMenuItem objects
    public ICollection<GlutenFreeMenuItem> GlutenFreeDishes { get; set; } // Collection of GlutenFreeMenuItem objects
    public ICollection<SpicyMenuItem> SpicyDishes { get; set; } // Collection of SpicyMenuItem objects

    // Constructor for creating a new PreorderedDish
    public PreorderedDish()
    {
        VeganDishes = new List<VeganMenuItem>(); // Initialize the collections
        GlutenFreeDishes = new List<GlutenFreeMenuItem>(); // Initialize the collections
        SpicyDishes = new List<SpicyMenuItem>(); // Initialize the collections
    }

    // Copy constructor for cloning a PreorderedDish
    public PreorderedDish(PreorderedDish original)
    {
        // Copy scalar properties
        Id = original.Id;
        TableOrderId = original.TableOrderId;
        TableOrder = original.TableOrder;

        // Copy collections (shallow copy)
        VeganDishes = new List<VeganMenuItem>(original.VeganDishes);
        GlutenFreeDishes = new List<GlutenFreeMenuItem>(original.GlutenFreeDishes);
        SpicyDishes = new List<SpicyMenuItem>(original.SpicyDishes);
    }

    // Method to clone a PreorderedDish
    public PreorderedDish Clone()
    {
        return new PreorderedDish(this); // Calls the copy constructor
    }
}

