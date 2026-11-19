using System;
using System.Collections;
using System.Collections.Generic;

namespace Client.Models;

public class Products
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public ICollection<RecipeIngredients> RecipeIngredients { get; set; }
        = new List<RecipeIngredients>();
}
