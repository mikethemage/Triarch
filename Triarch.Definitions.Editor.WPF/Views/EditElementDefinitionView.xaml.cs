using System.Windows;
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
