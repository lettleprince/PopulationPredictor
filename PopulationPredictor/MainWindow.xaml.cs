// Group2: Jingfei Yao, Grace Pauly, Xiaotong Han.
using System.Windows;
using System.IO;
using System;
using System.Text;

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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int DAY_DIGITS = 3;

        private float result;
        private const string FILENAME = "Result.txt";

        private readonly StringBuilder output = new();
        private readonly string fullName;

        public MainWindow()
        {
            InitializeComponent();

            string filePath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "PopulationPredictor");

            if (!Directory.Exists(filePath))
            {
                _ = Directory.CreateDirectory(filePath);
            }

            fullName = Path.Combine(filePath, FILENAME);
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {

            int startNumber = int.Parse(StartNumber.Text);
            float percent = int.Parse(DailyIncrease.Text);
            int days = int.Parse(Days.Text);

            output.Append($"{Environment.NewLine}New predicting for {Environment.NewLine}" +
                $"  starting number:{startNumber}{Environment.NewLine}" +
                $"  daily increase :{percent}%{Environment.NewLine}" +
                $"  number of days :{days}{Environment.NewLine}");

            result = startNumber;

            for (int x = 0; x < days; x++)
            {
                result += result * (1 + percent / 100);
                List.Items.Add(result);
                output.Append($"Day {x+1,DAY_DIGITS}: {result}{Environment.NewLine}");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            File.AppendAllText(fullName, output.ToString());
            output.Clear();
            List.Items.Clear();
        }
    }
}
