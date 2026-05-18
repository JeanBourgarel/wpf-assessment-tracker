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
        public EditAssessmentWindow(string question, string defaultAnswer = "")
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtUnitName.SelectAll();
            txtUnitName.Focus();
        }

        public string Answer
        {
            get { return txtUnitName.Text; }
        }
    }
}
