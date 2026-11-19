using System;

namespace Client.Models;

public class Purchases
{
    public Guid Id { get; set; }
    public Guid IngredientId { get; set; }
    public Ingredients Ingredient { get; set; } = null!;
    public DateTime PurchaseDate { get; set; }
    public Decimal QuantityKg { get; set; }
}
