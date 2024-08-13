using System.Windows;
using Triarch.Prototype.ViewModels;


namespace Triarch.Prototype;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.DataContext = new MainWindowViewModel { CloseAction = Close };
    }
}