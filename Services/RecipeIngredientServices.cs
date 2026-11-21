using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Microsoft.EntityFrameworkCore;

namespace Client.Services;

public class RecipeIngredientServices
{
    private readonly BakeryDbContext _context;

    public RecipeIngredientServices(BakeryDbContext context)
    {
        _context = context;
    }

    public async Task<List<RecipeIngredients>> GetAll()
    {
        return await _context.RecipeIngredients
            .Include(r => r.Product)
            .Include(r => r.Ingredient)
            .ToListAsync();
    }

    public async Task Add(RecipeIngredients recipeIngredient)
    {
        _context.RecipeIngredients.Add(recipeIngredient);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid productId, Guid ingredientId)
    {
        var recipeIngredient = await _context.RecipeIngredients
            .FindAsync(productId, ingredientId);

        if (recipeIngredient != null)
        {
            _context.RecipeIngredients.Remove(recipeIngredient);
            await _context.SaveChangesAsync();
        }
    }
}
