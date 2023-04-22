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
using Task2_GreetingLibrary;

namespace Task1_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConcatenationService _concatenationService = new ConcatenationService();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text;
            var message = _concatenationService.GetMessage(DateTime.Now, username);
            MessageBox.Show(message, "Hello World", MessageBoxButton.OK);
        }
    }
}
