using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFAssessmentTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string textFile = "assessments.txt";

        private List<string[]> assessmentList = new List<string[]>();
        public MainWindow()
        {
            InitializeComponent();
            ReadFromFile();
            DisplayAssessments();
        }

        private void ReadFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(textFile))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 5)
                        {
                            assessmentList.Add(new string[] { parts[0], parts[1], parts[2], parts[3], parts[4] });
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void DisplayAssessments()
        {
            lvwAssessments.Items.Clear();

            foreach (var assessment in assessmentList)
            {
                var displayItem = new
                {
                    //Data = assessment,
                    UnitName = assessment[0],
                    AssessmentName = assessment[1],
                    Type = assessment[2],
                    DueDate = assessment[3],
                    Grade = assessment[4],
                };
                lvwAssessments.Items.Add(displayItem);
            }
        }

        private void WriteToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(textFile))
                {
                    foreach (var exp in assessmentList)
                    {
                        writer.WriteLine($"{exp[0]}|{exp[1]}|{exp[2]}");
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("aaaaaaaaaaaaaa", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void BtnSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("aaaaaaaaaaaaaa", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnDeleteExpense_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string[] selectedExpense)
            {
                assessmentList.Remove(selectedExpense);
                WriteToFile();
                DisplayAssessments();
            }
        }

        private void BtnAddAssessment_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("add assessment", "Input Error",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}