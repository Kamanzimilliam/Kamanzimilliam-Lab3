namespace SmartRecipeBook.Models;

public class Ingredient
{
    public string Name { get; set; } = "";
    public string Quantity { get; set; } = "";
}

public class Recipe
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Title { get; set; } = "";
    public string Instructions { get; set; } = "";
    public string? PhotoPath { get; set; }
    public string Status { get; set; } = "DRAFT";
    public string OwnerName { get; set; } = "";
    public List<Ingredient> Ingredients { get; set; } = new();
}
