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
    public partial class SaveFileDialog : Window
    {
        public enum SaveResult { Cancel, CurrentFile, NewFile }
        public SaveResult Result = SaveResult.Cancel;
        public SaveFileDialog()
        {
            InitializeComponent();
        }

        private void Current_Click(object sender, RoutedEventArgs e)
        {
            Result = SaveResult.CurrentFile;
            this.DialogResult = true;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Result = SaveResult.NewFile;
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Result = SaveResult.Cancel;
            this.DialogResult = false;
        }
    }
}
