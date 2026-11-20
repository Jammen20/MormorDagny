using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Microsoft.EntityFrameworkCore;

namespace Client.Services;

public class IngredientServices
{
    private readonly BakeryDbContext _context;

    public IngredientServices(BakeryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ingredients>> GetAll()
    {
        return await _context.Ingredients.ToListAsync();
    }

    public async Task Add(Ingredients Ingredient)
    {
        _context.Ingredients.Add(Ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var Ingredient = await _context.Ingredients.FindAsync(id);

        if (Ingredient != null)
        {
            _context.Ingredients.Remove(Ingredient);
            await _context.SaveChangesAsync();
        }
    }
}
