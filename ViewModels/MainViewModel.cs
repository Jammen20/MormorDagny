using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? currentViewModel;

    public MainViewModel()
    {
        CurrentViewModel = new SuppliersViewModel();
    }

    [RelayCommand]
    private void ShowSuppliers()
    {
        CurrentViewModel = new SuppliersViewModel();
    }

    [RelayCommand]
    private void ShowIngredients()
    {
        CurrentViewModel = new IngredientsViewModel();
    }

    [RelayCommand]
    private void ShowProducts()
    {
        CurrentViewModel = new ProductsViewModel();
    }

    [RelayCommand]
    private void ShowRecipeIngredients()
    {
        CurrentViewModel = new RecipeIngredientsViewModel();
    }
}