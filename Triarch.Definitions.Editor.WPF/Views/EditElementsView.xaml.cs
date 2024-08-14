using System.Windows;
using Triarch.Definitions.Editor.WPF.ViewModels;

namespace Triarch.Definitions.Editor.WPF.Views;
/// <summary>
/// Interaction logic for EditElementsView.xaml
/// </summary>
public partial class EditElementsView : Window
{
    public EditElementsView()
    {
        InitializeComponent();
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditElementsViewModel)DataContext).Create();
        ((EditElementsViewModel)DataContext).RequeryList();
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditElementsViewModel)DataContext).Edit();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditElementsViewModel)DataContext).Delete();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
