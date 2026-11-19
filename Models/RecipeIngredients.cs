using System;

namespace Client.Models;

public class RecipeIngredients
{
    public Guid ProductId { get; set; }
    public Products Product { get; set; } = null!;
    public Guid IngredientId { get; set; }
    public Ingredients Ingredient { get; set; } = null!;
    public decimal AmountKg { get; set; }
}
