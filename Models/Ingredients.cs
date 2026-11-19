using System;

namespace Client.Models;

public class Ingredients
{
    public Guid Id { get; set; }
    public string ArticleNumber { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal PricePerKg { get; set; }
    public double StockQuantityKg { get; set; }
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
}
