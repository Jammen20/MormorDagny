using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class PurchasesViewModel : ViewModelBase
{
    private readonly PurchaseServices _purchaseService;
    private readonly IngredientServices _ingredientService;

    [ObservableProperty]
    private Ingredients? selectedIngredient;

    [ObservableProperty]
    private string quantityKg = "";

    public ObservableCollection<Purchases> Purchases { get; } = [];

    public ObservableCollection<Ingredients> Ingredients { get; } = [];

    public PurchasesViewModel()
    {
        _purchaseService = new PurchaseServices(new BakeryDbContext());
        _ingredientService = new IngredientServices(new BakeryDbContext());

        _ = LoadData();
    }

    [RelayCommand]
    private async Task LoadData()
    {
        var purchases = await _purchaseService.GetAll();
        var ingredients = await _ingredientService.GetAll();

        Purchases.Clear();
        Ingredients.Clear();

        foreach (var purchase in purchases)
            Purchases.Add(purchase);

        foreach (var ingredient in ingredients)
            Ingredients.Add(ingredient);
    }

    [RelayCommand]
    private async Task AddPurchase()
    {
        if (SelectedIngredient is null)
            return;

        if (string.IsNullOrWhiteSpace(QuantityKg))
            return;

        var quantity = double.Parse(
            QuantityKg.Replace(',', '.'),
            CultureInfo.InvariantCulture);

        var purchase = new Purchases
        {
            Id = Guid.NewGuid(),
            IngredientId = SelectedIngredient.Id,
            QuantityKg = quantity,
            PurchaseDate = DateTime.UtcNow
        };

        await _purchaseService.Add(purchase);

        // Uppdatera lagersaldo
        SelectedIngredient.StockQuantityKg += quantity;

        using var context = new BakeryDbContext();

        var ingredient = await context.Ingredients.FindAsync(SelectedIngredient.Id);

        if (ingredient != null)
        {
            ingredient.StockQuantityKg += quantity;

            await context.SaveChangesAsync();
        }

        QuantityKg = "";
        SelectedIngredient = null;

        await LoadData();
    }
}