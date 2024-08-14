using System.Windows;
using Triarch.Definitions.Editor.WPF.ViewModels;

namespace Triarch.Definitions.Editor.WPF.Views;
/// <summary>
/// Interaction logic for EditProgressionsView.xaml
/// </summary>
public partial class EditProgressionsView : Window
{
    public EditProgressionsView()
    {
        InitializeComponent();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionsViewModel)DataContext).Save();
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionsViewModel)DataContext).Create();
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionsViewModel)DataContext).Edit();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionsViewModel)DataContext).Delete();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionsViewModel)DataContext).CancelEdit();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void EditDefsButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionsViewModel)DataContext).EditDefinitions();
    }
}
