using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Microsoft.EntityFrameworkCore;

namespace Client.Services;

public class SupplierService
{
    private readonly BakeryDbContext _context;

    public SupplierService(BakeryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Supplier>> GetAll()
    {
        return await _context.Suppliers.ToListAsync();
    }

    public async Task Add(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);

        if (supplier != null)
        {
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }
    }
}
