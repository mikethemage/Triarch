using System.Windows;
using Triarch.Definitions.Editor.WPF.ViewModels;

namespace Triarch.Definitions.Editor.WPF.Views;
/// <summary>
/// Interaction logic for EditProgressionDefinitionView.xaml
/// </summary>
public partial class EditProgressionDefinitionView : Window
{
    public EditProgressionDefinitionView()
    {
        InitializeComponent();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionDefinitionViewModel)DataContext).Save();
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionDefinitionViewModel)DataContext).Create();
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionDefinitionViewModel)DataContext).Edit();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionDefinitionViewModel)DataContext).Delete();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionDefinitionViewModel)DataContext).CancelEdit();
    }

    private void MoveUpButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionDefinitionViewModel)DataContext).MoveUp();
    }

    private void MoveDownButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditProgressionDefinitionViewModel)DataContext).MoveDown();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
