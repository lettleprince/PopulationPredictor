// Group2: Jingfei Yao, Grace Pauly, Xiaotong Han.
using System.Windows;

/*
 * Create an application that predicts the approximate size of a population of organisms. 
 * The application should use textboxes to allow the user to enter the starting number of organisms,
 * the average daily population increase (as a percentage), 
 * and the number of days the organisms will be left to multiply.
 * 
 * Use a ListBox to display the population size for each day of the simulation.
 * 
 * If the user clicks the Save button, save the current prediction to a file.
*/
namespace PopulationPredictor
{
    public partial class MainWindow : Window
    {
        private readonly ViewModel vm = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;

            vm.SetupFile();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            vm.Calc();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            vm.Save();
        }
    }
}
