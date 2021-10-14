// Group2: Jingfei Yao, Grace Pauly, Xiaotong Han.
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
        private const string invalidErrorMessage = "Please input valid values!";

        public ObservableCollection<string> Items { get; } = new();

        private int startNumber;
        public string StartNumber
        {
            get { return startNumber > 0 ? startNumber.ToString() : ""; }
            set
            {
                bool canParse = int.TryParse(value, out int result);
                startNumber = canParse ? result : 0;
            }
        }

        private float percentage;
        public string Percentage
        {
            get { return percentage > 0 ? percentage.ToString() : ""; }
            set
            {
                bool canParse = float.TryParse(value, out float result);
                percentage = canParse ? result : 0;
            }
        }

        private int days;
        public string Days
        {
            get { return days > 0 ? days.ToString() : ""; }
            set
            {
                bool canParse = int.TryParse(value, out int result);
                days = canParse ? result : 0;
            }
        }

        private const int DAY_DIGITS = 2;
        private const string FILENAME = "Result.txt";

        private float result;
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
            if (startNumber <= 0 || percentage <= 0 || days <= 0)
            {
                MessageBox.Show(invalidErrorMessage);
                return;
            }

            output.Clear();
            Items.Clear();

            output.Append($"{Environment.NewLine}New predicting for {Environment.NewLine}" +
                $"  starting number:{startNumber}{Environment.NewLine}" +
                $"  daily increase :{percentage}%{Environment.NewLine}" +
                $"  number of days :{days}{Environment.NewLine}");

            result = startNumber;

            for (int x = 0; x < days; x++)
            {
                result += result * (percentage / 100);
                string resultString = $"Day{x + 1,DAY_DIGITS}: {Math.Round(result)}";
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
