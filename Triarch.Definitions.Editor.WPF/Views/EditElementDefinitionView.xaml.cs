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
using Triarch.Definitions.Editor.WPF.ViewModels;

namespace Triarch.Definitions.Editor.WPF.Views;
/// <summary>
/// Interaction logic for EditElementDefinitionView.xaml
/// </summary>
public partial class EditElementDefinitionView : Window
{
    public EditElementDefinitionView()
    {
        InitializeComponent();
    }    

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditElementDefinitionViewModel)DataContext).Save();
    }

    private void CustomProgressionButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditElementDefinitionViewModel)DataContext).EditCustomProgression();
    }
}
