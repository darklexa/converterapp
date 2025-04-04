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

    //This is the event handler for the button click
    private void ConvertButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Button clicked!");
    }

    //This is the event handler for the second button, header
    private void HeaderButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Header button clicked!");
    }
}