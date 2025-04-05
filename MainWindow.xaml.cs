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


namespace MyWpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // When the user clicks "Doc to Docx," load the DocToDocxControl into ContentArea
    private void DocToDocxButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("You have opened DocToDocx page!");
        ContentArea.Content = new DocToDocxControl();
    }

    // When the user clicks "Header Seperator," load the HeaderSeperatorControl into ContentArea
    private void HeaderSeperatorButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("You have opened Header Seperator page!");
        ContentArea.Content = new HeaderSeperator();
    }
}