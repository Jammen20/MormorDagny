using System;
using System.Dynamic;
using Client.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Client.Data;

public class BakeryDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseNpgsql(
            config.GetConnectionString("DefaultConnection"));

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeIngredients>()
            .HasKey(x => new
            {
                x.ProductId,
                x.IngredientId
            });
    }

    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Ingredients> Ingredients => Set<Ingredients>();
    public DbSet<Products> Products => Set<Products>();
    public DbSet<RecipeIngredients> RecipeIngredients => Set<RecipeIngredients>();
    public DbSet<Purchases> Purchases => Set<Purchases>();
}
