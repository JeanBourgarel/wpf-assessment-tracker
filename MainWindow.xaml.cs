using Microsoft.VisualBasic;
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
        private List<string[]> lastSavedAssessmentList = new List<string[]>();
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
                    assessmentList.Clear();
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
                lastSavedAssessmentList = assessmentList.ToList();
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void DisplayAssessmentPerColumn()
        {

        }

        private void DisplayAssessments()
        {
            lvwAssessments.Items.Clear();
            lvwAssessmentsCompleted.Items.Clear();

            foreach (var assessment in assessmentList)
            {
                var displayItem = new
                {
                    Data = assessment,
                    UnitName = assessment[0],
                    AssessmentName = assessment[1],
                    Type = assessment[2],
                    DueDate = assessment[3],
                    Grade = assessment[4],
                };

                ListView selectedList = displayItem.Grade == "S" ? lvwAssessmentsCompleted : lvwAssessments;
                string searchBoxText = txtSearch.Text;
                string filterBy = comboBxFilter.Text;

                if (searchBoxText != "")
                {
                    if (filterBy == "Unit" && displayItem.UnitName.Contains(searchBoxText, StringComparison.OrdinalIgnoreCase))
                    {
                        selectedList.Items.Add(displayItem);
                    } else if (filterBy == "Type" && displayItem.Type.Contains(searchBoxText, StringComparison.OrdinalIgnoreCase))
                    {
                        selectedList.Items.Add(displayItem);
                    }
                    else if (filterBy == "Name" && displayItem.AssessmentName.Contains(searchBoxText, StringComparison.OrdinalIgnoreCase))
                    {
                        selectedList.Items.Add(displayItem);
                    }
                } else
                {
                    selectedList.Items.Add(displayItem);
                }

            }

            tbkCurrentFile.Text = "Current file: " + textFile;
        }

        private bool WriteToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(textFile))
                {
                    foreach (var exp in assessmentList)
                    {
                        writer.WriteLine($"{exp[0]}|{exp[1]}|{exp[2]}|{exp[3]}|{exp[4]}");
                    }
                }
                lastSavedAssessmentList = assessmentList;
                return true;
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
        }

        private void txtSearchChangedEventHandler(object sender, TextChangedEventArgs e)
        {
            string currentText = txtSearch.Text;
            DisplayAssessments();
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                if (hasUnsavedChanges())
                {
                    MessageBoxResult saveResult = MessageBox.Show("Would you like to save your changes before loading another file ?",
                        "Confirm Save",
                        MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question);

                    if (saveResult == MessageBoxResult.Yes)
                    {
                        if (WriteToFile() == true)
                        {
                            MessageBox.Show("Changes saved successfully to " + textFile + ".", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else if (saveResult == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                }
                string fileName = dialog.FileName;
                textFile = System.IO.Path.GetFileName(fileName);
                ReadFromFile();
                DisplayAssessments();
            }
        }
        private void BtnSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to save your changes?", "Confirm Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (WriteToFile() == true)
                {
                    MessageBox.Show("Changes saved successfully to " + textFile + ".", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BtnDeleteAssessment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string[] selectedExpense)
            {
                assessmentList.Remove(selectedExpense);
                // WriteToFile();
                DisplayAssessments();
            }
        }

        private void BtnAddAssessment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUnitName.Text)
                 || string.IsNullOrWhiteSpace(txtAssessmentName.Text)
                 || string.IsNullOrWhiteSpace(txtType.Text)
                 || dpkDueDate.SelectedDate == null
                 || string.IsNullOrWhiteSpace(txtGrade.Text))
            {
                MessageBox.Show("Please fill in Unit name, Assessment name, Type, Due date and Grade.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string[] row = new string[]
            {
                txtUnitName.Text.Trim(),
                txtAssessmentName.Text.Trim(),
                txtType.Text.Trim(),
                dpkDueDate.SelectedDate.Value.ToString("yyyy-MM-dd"),
                txtGrade.Text.Trim(),
            };
            assessmentList.Add(row);
            //WriteToFile();
            DisplayAssessments();
        }

        private bool hasUnsavedChanges()
        {
            if (lastSavedAssessmentList.Count != assessmentList.Count)
            {
                return true;
            }

            for (int i = 0; i < assessmentList.Count; i++)
            {
                if (assessmentList[i] != lastSavedAssessmentList[i])
                {
                    return true;
                }
            }
            return false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!hasUnsavedChanges())
            {
                return;
            }
            MessageBoxResult result = MessageBox.Show("Would you like to save your changes before quitting?", "Confirm Save", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel)
            {
                // Stops the window from closing
                e.Cancel = true;
            }
            else if (result == MessageBoxResult.Yes)
            {
                if (WriteToFile() == true)
                {
                    MessageBox.Show("Changes saved successfully to " + textFile + ".", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}