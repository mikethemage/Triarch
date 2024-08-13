using System.Windows.Controls;
using System.Windows.Input;
using Triarch.Prototype.ViewModels;

namespace Triarch.Prototype.Views;
/// <summary>
/// Interaction logic for EntityEditorView.xaml
/// </summary>
public partial class EntityEditorView : UserControl
{
    public EntityEditorView()
    {
        InitializeComponent();
    }

    private void AddChildListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (((EntityEditorViewModel)DataContext).CanAdd())
        {
            ((EntityEditorViewModel)DataContext).Add();
        }
    }

    private void AddChildListBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Return)
        {
            if (((EntityEditorViewModel)DataContext).CanAdd())
            {
                ((EntityEditorViewModel)DataContext).Add();
            }
        }
    }
}
