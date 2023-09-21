using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Triarch.RPGSystem.Editor.WPF.ViewModels;
using Triarch.RPGSystem.Editor.WPF.Views;

namespace Triarch.RPGSystem.Editor.WPF;
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
