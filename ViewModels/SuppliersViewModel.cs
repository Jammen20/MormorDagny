using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Client.Data;
using Client.Models;
using Client.Services;
using System.Threading.Tasks;
using System;

namespace Client.ViewModels;

public partial class SuppliersViewModel : ViewModelBase
{
    private readonly SupplierServices _supplierService;

    [ObservableProperty]
    private string name = "";

    [ObservableProperty]
    private string address = "";

    [ObservableProperty]
    private string contactPerson = "";

    [ObservableProperty]
    private string phoneNumber = "";

    [ObservableProperty]
    private string email = "";

    public ObservableCollection<Supplier> Suppliers { get; } = [];

    public SuppliersViewModel()
    {
        _supplierService = new SupplierServices(new BakeryDbContext());

        Task.Run(LoadSuppliers);
    }

    [RelayCommand]
    private async Task LoadSuppliers()
    {
        var suppliers = await _supplierService.GetAll();

        Suppliers.Clear();

        foreach (var supplier in suppliers)
        {
            Suppliers.Add(supplier);
        }
    }

    [RelayCommand]
    private async Task AddSupplier()
    {
        var supplier = new Supplier
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Address = Address,
            ContactPerson = ContactPerson,
            PhoneNumber = PhoneNumber,
            Email = Email
        };

        await _supplierService.Add(supplier);

        Name = "";
        Address = "";
        ContactPerson = "";
        PhoneNumber = "";
        Email = "";

        await LoadSuppliers();
    }
}