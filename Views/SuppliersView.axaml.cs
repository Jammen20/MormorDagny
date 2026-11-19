using Avalonia.Controls;
using Client.ViewModels;

namespace Client.Views;

public partial class SuppliersView : UserControl
{
    public SuppliersView()
    {
        InitializeComponent();
        DataContext = new SuppliersViewModel();
    }
}