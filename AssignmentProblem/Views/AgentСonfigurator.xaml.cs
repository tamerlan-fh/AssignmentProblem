using System.Windows;

namespace AssignmentProblem.Views
{
    /// <summary>
    /// Логика взаимодействия для Agents.xaml
    /// </summary>
    public partial class AgentСonfigurator : Window
    {
        public AgentСonfigurator()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
