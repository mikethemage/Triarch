using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;
using Triarch.BusinessLogic.Services;
using Triarch.Dtos.Definitions;
using Triarch.Dtos.Entities;
using Triarch.Prototype.ViewModels;
using Triarch.Prototype.Views;

namespace Triarch.Prototype.Views;
/// <summary>
/// Interaction logic for MainMenuView.xaml
/// </summary>
public partial class MainMenuView : UserControl
{
    public MainMenuView()
    {
        InitializeComponent();        
    }    
}
