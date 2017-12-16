using System.Windows;

namespace AssignmentProblem.Views
{
    /// <summary>
    /// Логика взаимодействия для OperationConfigurator.xaml
    /// </summary>
    public partial class OperationConfigurator : Window
    {
        public OperationConfigurator()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
