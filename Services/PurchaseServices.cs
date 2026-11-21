using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Microsoft.EntityFrameworkCore;

namespace Client.Services;

public class PurchaseServices
{
    private readonly BakeryDbContext _context;

    public PurchaseServices(BakeryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Purchases>> GetAll()
    {
        return await _context.Purchases
            .Include(p => p.Ingredient)
            .ToListAsync();
    }

    public async Task Add(Purchases purchase)
    {
        _context.Purchases.Add(purchase);

        await _context.SaveChangesAsync();
    }
}