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

namespace Triarch.Definitions.Editor.WPF.Views
{
    /// <summary>
    /// Interaction logic for CreateSystemRulesetPromptView.xaml
    /// </summary>
    public partial class CreateSystemRulesetPromptView : Window
    {
        public CreateSystemRulesetPromptView()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = ((CreateSystemRulesetPromptViewModel)DataContext).GetSelectedRuleset();
            if (a != null )
            {
                EditSystemViewModel b = new(((CreateSystemRulesetPromptViewModel)DataContext).GetDbContext(), a);
                Close();
                b.ShowWindow();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
