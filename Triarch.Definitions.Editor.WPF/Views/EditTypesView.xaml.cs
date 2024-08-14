using System.Windows;
using Triarch.Definitions.Editor.WPF.ViewModels;

namespace Triarch.Definitions.Editor.WPF.Views;
/// <summary>
/// Interaction logic for EditTypesView.xaml
/// </summary>
public partial class EditTypesView : Window
{
    public EditTypesView()
    {
        InitializeComponent();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditTypesViewModel)DataContext).Save();
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditTypesViewModel)DataContext).Create();
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditTypesViewModel)DataContext).Edit();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditTypesViewModel)DataContext).Delete();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditTypesViewModel)DataContext).CancelEdit();
    }

    private void MoveUpButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditTypesViewModel)DataContext).MoveUp();
    }

    private void MoveDownButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditTypesViewModel)DataContext).MoveDown();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
