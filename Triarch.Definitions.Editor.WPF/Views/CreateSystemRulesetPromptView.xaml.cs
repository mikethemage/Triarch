using System.Windows;
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
            if (a != null)
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
