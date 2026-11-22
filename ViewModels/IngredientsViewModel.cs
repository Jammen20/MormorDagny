using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

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
    private string pricePerKg = "";
    [ObservableProperty]
    private string stockQuantityKg = "";
    [ObservableProperty]
    private Supplier? selectedSupplier;

    public ObservableCollection<Ingredients> Ingredients { get; } = [];
    public ObservableCollection<Supplier> Suppliers { get; } = [];

    public IngredientsViewModel()
    {
        _ingredientService = new IngredientServices(new BakeryDbContext());
        _supplierService = new SupplierServices(new BakeryDbContext());

        _ = LoadData();
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
        if (string.IsNullOrWhiteSpace(ArticleNumber))
            return;

        if (string.IsNullOrWhiteSpace(Name))
            return;

        if (string.IsNullOrWhiteSpace(PricePerKg))
            return;

        if (string.IsNullOrWhiteSpace(StockQuantityKg))
            return;

        if (SelectedSupplier is null)
            return;

        if (SelectedSupplier is null)
            return;

        var ingredient = new Ingredients
        {
            Id = Guid.NewGuid(),
            ArticleNumber = ArticleNumber,
            Name = Name,
            PricePerKg = decimal.Parse(PricePerKg.Replace(',', '.'), CultureInfo.InvariantCulture),
            StockQuantityKg = double.Parse(StockQuantityKg.Replace(',', '.'), CultureInfo.InvariantCulture),
            SupplierId = SelectedSupplier.Id
        };

        await _ingredientService.Add(ingredient);

        await LoadData();
    }
}
