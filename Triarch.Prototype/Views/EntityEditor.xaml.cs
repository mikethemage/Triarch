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
using Triarch.Prototype.ViewModels;

namespace Triarch.Prototype.Views;
/// <summary>
/// Interaction logic for EntityEditor.xaml
/// </summary>
public partial class EntityEditor : Window
{
    public EntityEditor(EntityViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void AddChildListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if(((EntityViewModel)DataContext).CanAdd())
        {
            ((EntityViewModel)DataContext).Add();
        }
    }

    private void AddChildListBox_KeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Return)
        {
            if (((EntityViewModel)DataContext).CanAdd())
            {
                ((EntityViewModel)DataContext).Add();
            }
        }
    }
}
