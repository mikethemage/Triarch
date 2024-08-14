using System.Windows;
using Triarch.Definitions.Editor.WPF.ViewModels;

namespace Triarch.Definitions.Editor.WPF.Views;
/// <summary>
/// Interaction logic for EditSystemView.xaml
/// </summary>
public partial class EditSystemView : Window
{
    public EditSystemView()
    {
        InitializeComponent();
        // Disable buttons initially until Save button is pressed
        DisableButtons();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        if (((EditSystemViewModel)DataContext).Saved == false)
        {
            if (MessageBox.Show("Close without saving?", "Close Form", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
        }
        Close();
    }

    private void DisableButtons()
    {
        CreateEditTypesButton.IsEnabled = false;
        CreateEditAttributesButton.IsEnabled = false;
        CreateEditGenresButton.IsEnabled = false;
        CreateEditProgressionsButton.IsEnabled = false;
        CloseButton.IsEnabled = false;
    }

    private void EnableButtons()
    {
        CreateEditTypesButton.IsEnabled = true;
        CreateEditAttributesButton.IsEnabled = true;
        CreateEditGenresButton.IsEnabled = true;
        CreateEditProgressionsButton.IsEnabled = true;
        CloseButton.IsEnabled = true;
    }

    private void LockForm()
    {
        ((EditSystemViewModel)DataContext).LockForm();
    }

    private void UnlockForm()
    {
        ((EditSystemViewModel)DataContext).UnlockForm();
    }

    private void UnlockButton_Click(object sender, RoutedEventArgs e)
    {
        UnlockForm(); // Unlock the form and re-disable buttons
        DisableButtons();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        ((EditSystemViewModel)DataContext).Save();
        LockForm(); // Lock the form after saving
        EnableButtons(); // Enable buttons after Save is pressed
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
