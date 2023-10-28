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
