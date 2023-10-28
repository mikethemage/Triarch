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
/// Interaction logic for EditSystemView.xaml
/// </summary>
public partial class EditSystemView : Window
{
    public EditSystemView()
    {
        InitializeComponent();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        if(((EditSystemViewModel)DataContext).Saved == false)
        {
            if( MessageBox.Show("Close without saving?","Close Form",MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
        }
        Close();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        
            ((EditSystemViewModel)DataContext).Save();
        
    }

    private void CreateEditTypesButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditSystemViewModel)DataContext).CreateEditTypes();
    }

    private void CreateEditAttributesButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditSystemViewModel)DataContext).CreateEditElements();
    }

    private void CreateEditGenresButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditSystemViewModel)DataContext).CreateEditGenres();
    }

    private void CreateEditProgressionsButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditSystemViewModel)DataContext).CreateEditProgressions();
    }
}
