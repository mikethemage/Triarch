using System.Windows;
using Triarch.Definitions.Editor.WPF.ViewModels;

namespace Triarch.Definitions.Editor.WPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new RPGSystemSelectViewModel();
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        CreateSystemRulesetPromptViewModel b = new(((RPGSystemSelectViewModel)DataContext).GetDbContext());
        b.ShowWindow();
        ((RPGSystemSelectViewModel)DataContext).RequeryList();
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedItem = ((RPGSystemSelectViewModel)DataContext).SelectedItem;
        if (selectedItem != null)
        {
            EditSystemViewModel b = new(((RPGSystemSelectViewModel)DataContext).GetDbContext(), selectedItem.Id);
            b.ShowWindow();
            ((RPGSystemSelectViewModel)DataContext).RequeryList();
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        ((RPGSystemSelectViewModel)DataContext).Delete();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
