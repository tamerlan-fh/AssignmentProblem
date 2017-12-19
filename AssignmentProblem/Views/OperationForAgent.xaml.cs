using System.Windows;
using AssignmentProblem.ViewModels;

namespace AssignmentProblem.Views
{
    /// <summary>
    /// Логика взаимодействия для OperationForAgent.xaml
    /// </summary>
    public partial class OperationForAgent : Window
    {
        public OperationForAgent(AgentViewModel agent)
        {
            InitializeComponent();
            this.DataContext = agent;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
