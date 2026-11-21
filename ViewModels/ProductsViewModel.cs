using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Client.Data;
using Client.Models;
using Client.Services;
using System.Threading.Tasks;
using System;

namespace Client.ViewModels;

public partial class ProductsViewModel : ViewModelBase
{
    private readonly ProductServices _productService;

    [ObservableProperty]
    private string name = "";

    public ObservableCollection<Products> Products { get; } = [];

    public ProductsViewModel()
    {
        _productService = new ProductServices(new BakeryDbContext());

        _ = LoadProducts();
    }

    [RelayCommand]
    private async Task LoadProducts()
    {
        var products = await _productService.GetAll();

        Products.Clear();

        foreach (var product in products)
        {
            Products.Add(product);
        }
    }

    [RelayCommand]
    private async Task AddProduct()
    {
        var product = new Products
        {
            Id = Guid.NewGuid(),
            Name = Name,
        };

        await _productService.Add(product);

        Name = "";

        await LoadProducts();
    }

}
