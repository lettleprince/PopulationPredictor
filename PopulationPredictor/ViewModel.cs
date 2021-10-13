using System;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;

namespace PopulationPredictor
{
    class ViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<string> items = new();
        public ObservableCollection<string> Items => items;

        private int startNumber;
        public int StartNumber
        {
            get { return startNumber; }
            set { startNumber = value; propertyChanged(); }
        }

        private float percentage;
        public float Percentage
        {
            get { return percentage; }
            set { percentage = value; propertyChanged(); }
        }

        private int days;
        public int Days
        {
            get { return days; }
            set { days = value; propertyChanged(); }
        }

        private const int DAY_DIGITS = 3;

        private float result;
        private const string FILENAME = "Result.txt";

        private readonly StringBuilder output = new();
        private string fullName;

        public void SetupFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "PopulationPredictor");

            if (!Directory.Exists(filePath))
            {
                _ = Directory.CreateDirectory(filePath);
            }

            fullName = Path.Combine(filePath, FILENAME);
        }

        public void Calc()
        {
            output.Clear();
            Items.Clear();

            output.Append($"{Environment.NewLine}New predicting for {Environment.NewLine}" +
                $"  starting number:{StartNumber}{Environment.NewLine}" +
                $"  daily increase :{Percentage}%{Environment.NewLine}" +
                $"  number of days :{Days}{Environment.NewLine}");

            result = StartNumber;

            for (int x = 0; x < days; x++)
            {
                result += result * (Percentage / 100);
                string resultString = $"Day {x + 1,DAY_DIGITS}: {result}";
                Items.Add(resultString);
                output.Append($"{resultString}{Environment.NewLine}");
            }
        }

        public void Save()
        {
            string message;
            if (output.Length > 0)
            {
                File.AppendAllText(fullName, output.ToString());
                message = $"Results have been saved to file: ${fullName}";

                output.Clear();
                Items.Clear();

                StartNumber = 0;
                Percentage = 0;
                Days = 0;
            }
            else
            {
                message = "Please input valid values!";
            }

            MessageBox.Show(message);
        }

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        private void propertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
