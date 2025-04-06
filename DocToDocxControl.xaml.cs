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
using Microsoft.Office.Interop.Word;
using System.IO;
using IOPath = System.IO.Path;
using WordApp = Microsoft.Office.Interop.Word.Application;
using WordDoc = Microsoft.Office.Interop.Word.Document;




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

        // Start the conversion process
        private void StartConversionButton_Click(object sender, RoutedEventArgs e)
        {
            // output directory
            string outputFolder = @"C:\ConvertedFiles";

            // create output directory if it doesn't exist
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // loop through each selected file and CONVERT
            for (int i = 0; i < selectedFiles.Count; i++)
                {
                    string inputFile = selectedFiles[i];

                // change filename extension from .doc to .docx
                string outputFile = IOPath.Combine(outputFolder, IOPath.GetFileNameWithoutExtension(inputFile) + ".docx");

                // call the conversion helper method
                string result = ConvertDocToDocx(inputFile, outputFile);

                // update the status list
                StatusListBox.Items[i] = $"{inputFile} - {result}";

                }

            // Display the output location in the UI
            OutputLocationTextBlock.Text = $"Files saved to: {outputFolder}";
        }

        // Helper method to convert DOC to DOCX
        private string ConvertDocToDocx(string inputFile, string outputFile)
        {
            WordApp? wordApp = null;
            WordDoc? doc = null;

            try
            {
                // Create a new instance of Word
                wordApp = new WordApp();

                // open the doc file but do not make word visible
                doc = wordApp.Documents.Open(inputFile, ReadOnly: false, Visible: false);

                // Save the document as DOCX
                doc.SaveAs2(outputFile, WdSaveFormat.wdFormatXMLDocument);
                return "Converted successfully";

            }

            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

            finally
            {
                // close the document and quit Word
                if (doc != null)
                {
                    doc.Close();
                }
                if (wordApp != null)
                {
                    wordApp.Quit();
                }

            }





        }

    }
}
