using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class IngredientsViewModel : ViewModelBase
{
    private readonly IngredientServices _ingredientService;
    private readonly SupplierServices _supplierService;

    [ObservableProperty]
    private string articleNumber = "";

    [ObservableProperty]
    private string name = "";

    [ObservableProperty]
    private decimal pricePerKg;

    [ObservableProperty]
    private double stockQuantityKg;

    [ObservableProperty]
    private Supplier? selectedSupplier;

    public ObservableCollection<Ingredients> Ingredients { get; } = [];

    public ObservableCollection<Supplier> Suppliers { get; } = [];

    public IngredientsViewModel()
    {
        _ingredientService = new IngredientServices(new BakeryDbContext());
        _supplierService = new SupplierServices(new BakeryDbContext());

        Task.Run(LoadData);
    }

    [RelayCommand]
    private async Task LoadData()
    {
        var ingredients = await _ingredientService.GetAll();
        var suppliers = await _supplierService.GetAll();

        Ingredients.Clear();
        Suppliers.Clear();

        foreach (var ingredient in ingredients)
            Ingredients.Add(ingredient);

        foreach (var supplier in suppliers)
            Suppliers.Add(supplier);
    }

    [RelayCommand]
    private async Task AddIngredient()
    {
        if (SelectedSupplier is null)
            return;

        var ingredient = new Ingredients
        {
            Id = Guid.NewGuid(),
            ArticleNumber = ArticleNumber,
            Name = Name,
            PricePerKg = PricePerKg,
            StockQuantityKg = StockQuantityKg,
            SupplierId = SelectedSupplier.Id
        };

        await _ingredientService.Add(ingredient);

        await LoadData();
    }
}
