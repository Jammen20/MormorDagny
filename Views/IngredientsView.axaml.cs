using Avalonia.Controls;
using Client.ViewModels;

namespace Client.Views;

public partial class IngredientsView : UserControl
{
    public IngredientsView()
    {
        InitializeComponent();
        DataContext = new IngredientsViewModel();
    }
}