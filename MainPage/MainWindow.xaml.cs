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

namespace MainPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btLogin(object sender, RoutedEventArgs e)
        {
            Login loginpg = new Login();
            loginpg.Show();
        }

        private void btGuest(object sender, RoutedEventArgs e)
        {

        }

        private void btReg(object sender, RoutedEventArgs e)
        {

        }
    }
}