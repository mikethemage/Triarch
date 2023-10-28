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
using System.Windows.Shapes;
using Triarch.RPGSystem.Editor.WPF.ViewModels;

namespace Triarch.RPGSystem.Editor.WPF.Views;
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
