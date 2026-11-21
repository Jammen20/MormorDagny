using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class RecipeIngredientsViewModel : ViewModelBase
{
    private readonly RecipeIngredientServices _recipeIngredientService;
    private readonly IngredientServices _ingredientService;
    private readonly ProductServices _productService;

    [ObservableProperty]
    private Ingredients? selectedIngredient;
    [ObservableProperty]
    private Products? selectedProduct;
    [ObservableProperty]
    private string amountKg = "";

    public ObservableCollection<RecipeIngredients> RecipeIngredients { get; set; } = [];
    public ObservableCollection<Ingredients> Ingredients { get; set; } = [];
    public ObservableCollection<Products> Products { get; set; } = [];

    public RecipeIngredientsViewModel()
    {
        _recipeIngredientService = new RecipeIngredientServices(new BakeryDbContext());
        _ingredientService = new IngredientServices(new BakeryDbContext());
        _productService = new ProductServices(new BakeryDbContext());

        _ = LoadData();
    }

    [RelayCommand]
    private async Task LoadData()
    {
        var recipeIngredients = await _recipeIngredientService.GetAll();
        var ingredients = await _ingredientService.GetAll();
        var products = await _productService.GetAll();

        RecipeIngredients.Clear();
        Ingredients.Clear();
        Products.Clear();

        foreach (var recipeIngredient in recipeIngredients)
            RecipeIngredients.Add(recipeIngredient);

        foreach (var ingredient in ingredients)
            Ingredients.Add(ingredient);

        foreach (var product in products)
            Products.Add(product);
    }

    [RelayCommand]
    private async Task AddRecipeIngredient()
    {
        if (SelectedIngredient is null)
            return;
        if (SelectedProduct is null)
            return;
        var recipeIngredient = new RecipeIngredients
        {
            IngredientId = SelectedIngredient.Id,
            ProductId = SelectedProduct.Id,
            AmountKg = decimal.Parse(AmountKg.Replace(',', '.'), CultureInfo.InvariantCulture)
        };

        await _recipeIngredientService.Add(recipeIngredient);

        SelectedIngredient = null;
        AmountKg = "";

        await LoadData();
    }

    public IEnumerable<IGrouping<string, RecipeIngredients>> GroupedRecipes =>
    RecipeIngredients.GroupBy(r => r.Product.Name);
}
