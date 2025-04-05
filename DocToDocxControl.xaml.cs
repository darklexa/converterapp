using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWpfApp
{
    /// <summary>
    /// Interaction logic for DocToDocxControl.xaml
    /// </summary>
    public partial class DocToDocxControl : UserControl
    {
        // Declare selectedFiles as a class-level field.
        private List<string> selectedFiles = new List<string>();

        public DocToDocxControl()
        {
            InitializeComponent();
        }

        // Select DOC files
        private void SelectFilesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "DOC Files (*.doc)|*.doc",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFiles.Clear();
                selectedFiles.AddRange(openFileDialog.FileNames);
                SelectedFilesLabel.Text = $"{selectedFiles.Count} file(s) selected";

                // Update status list with each file marked as pending
                StatusListBox.Items.Clear();
                foreach (var file in selectedFiles)
                {
                    StatusListBox.Items.Add($"{file} - Pending");
                }
            }
        }

        // Simulate the conversion process
        private void StartConversionButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < selectedFiles.Count; i++)
            {
                // Here, you'd integrate real conversion logic
                StatusListBox.Items[i] = $"{selectedFiles[i]} - Converted successfully";
            }

            // Show output location
            OutputLocationTextBlock.Text = "Files saved to: C:\\ConvertedFiles";
        }
    }
}
