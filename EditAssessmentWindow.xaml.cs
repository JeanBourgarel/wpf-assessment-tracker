using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFAssessmentTracker
{
    /// <summary>
    /// Interaction logic for EditAssessmentWindow.xaml
    /// </summary>
    public partial class EditAssessmentWindow : Window
    {
        public string[] savedAssessment = new string[5];
        public EditAssessmentWindow(string[] assessment)
        {
            InitializeComponent();
            SetData(assessment);
        }

        private void SetData(string[] assessment)
        {
            for (int i = 0; i < assessment.Length; i++)
            {
                savedAssessment[i] = new string(assessment[i]);
            }

            txtUnitName.Text = savedAssessment[0];
            txtAssessmentName.Text = savedAssessment[1];
            txtType.Text = savedAssessment[2];
            dpkDueDate.Text = savedAssessment[3];
            comboBxGrade.SelectedValue = savedAssessment[4];
        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUnitName.Text)
                 || string.IsNullOrWhiteSpace(txtAssessmentName.Text)
                 || string.IsNullOrWhiteSpace(txtType.Text)
                 || dpkDueDate.SelectedDate == null
                 || string.IsNullOrWhiteSpace(comboBxGrade.Text))
            {
                MessageBox.Show("Please fill in Unit name, Assessment name, Type, Due date and Grade.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            savedAssessment[0] = txtUnitName.Text;
            savedAssessment[1] = txtAssessmentName.Text;
            savedAssessment[2] = txtType.Text;
            savedAssessment[3] = dpkDueDate.Text;
            savedAssessment[4] = comboBxGrade.Text;
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtUnitName.SelectAll();
            txtUnitName.Focus();
        }
    }
}
