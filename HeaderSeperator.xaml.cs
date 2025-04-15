using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for HeaderSeperator.xaml
    /// </summary>
    public partial class HeaderSeperator : UserControl
    {
        // allow one file for header extraction for simplicity
        private string selectedHeaderFile = string.Empty;

        public HeaderSeperator()
        {
            InitializeComponent();
        }

        private void SelectFilesHeaderButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Word Files (*.doc;*.docx)|*.doc;*docx",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedHeaderFile = openFileDialog.FileName;
                SelectedFilesLabel.Text = $"Selected: {System.IO.Path.GetFileName(selectedHeaderFile)}";
            }

        }


        // starting header extraction
        private async Task StartHeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedHeaderFile))
            {
                MessageBox.Show("Please select a file first.", "No file selected.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string outputFolder = @"C:\ExtractedHeaders";

            // ensure output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // progress reporter for ui
            var progressIndicator = new Progress<int>(percent =>
            {
                // percentage notification
                OutputLocationTextBlock.Text = $"Processing: {percent}% complete";

            });

            // run the header extraction on a background thread
            string result = await Task.Run(() => ExtractHeadersFromDoc(selectedHeaderFile, outputFolder, progressIndicator));

            // update the UI with the result
            OutputLocationTextBlock.Text = result;
        }


        private string ExtractHeadersFromDoc(string inputFile, string outputfolder, IProgress<int> progress)
        {
            WordApp wordApp = null;
            DocumentFormat doc = null;

            try
            {
                // open the doc file in read-only mode and invisible
                wordApp = new WordApp();
                doc = wordApp.Documents.Open(inputFile, ReadOnly: true, Visible: false);

                int sectionCount = doc.Sections.Count;
                if (sectionCount == 0)
                    return "No sections (and headers) found in the document.";

                // loop through each section
                for (int i = 1; i <= sectionCount; i++)
                {
                    Section section = doc.Sections[i];
                    //access primary header
                    HeaderFooter header = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary];

                    // only process if the header exist.
                    if (header.Exists)
                    {
                        // get header content text
                        string headerText = header.Range.Text.Trim();
                        if (!string.IsNullOrEmpty(headerText))
                        {
                            // create a new doc and insert the header text
                            Document newDoc = wordApp.Documents.Add();
                            newDoc.Content.Text = headerText;

                            // create a unique filename 
                            string outputFile = System.IO.Path.Combine(outputfolder, $"HeaderSection_{i}.docx");

                            // save the new document in docx format
                            newDoc.SaveAs2(outputFile, WdSaveFormat.wdFormatXMLDocument);
                            newDoc.Close();
                        }
                    }

                    // report progress as percentage
                    int percentComplete = (i * 100) / sectionCount;
                    progress.Report(percentComplete);

                    // simulate some work


                }

                return $"Header extraction complete. Files saved to: {outputfolder}";

            }

            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
            finally
            {
                if (doc != null)
                    doc.Close();
                if (wordApp !=null)
                    wordApp.Quit(); 
            }

        }



    }
}
