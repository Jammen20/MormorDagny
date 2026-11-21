using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Microsoft.EntityFrameworkCore;

namespace Client.Services;

public class ProductServices
{
    private readonly BakeryDbContext _context;

    public ProductServices(BakeryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Products>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task Add(Products product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}