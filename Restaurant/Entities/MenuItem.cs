using System;
namespace Restaurant.Entities;

public interface IMenuItem
{
    int Id { get; set; }
    string Name { get; set; }
    decimal Price { get; set; }
}

public class VeganMenuItem : IMenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
    public decimal Price { get; set; }
    public bool ContainsNuts { get; set; }
}

public class GlutenFreeMenuItem : IMenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
    public decimal Price { get; set; }
    public string AllergenInfo { get; set; }
}

public class SpicyMenuItem : IMenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
    public decimal Price { get; set; }
    public int SpiceLevel { get; set; }
}