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
using MainPage.Models;

namespace MainPage
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        private void btLogin(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }

        private void btGuest(object sender, RoutedEventArgs e)
        {
            var guestUser = new Felhasználó
            {
                Név = "Vendég",
                Email = "guest@guest.hu",
                Rang = "Vendég"
            };

            var mainPage = new Main(guestUser);
            this.NavigationService?.Navigate(mainPage);
        }

        private void btReg(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("RegisterPage.xaml", UriKind.Relative));
        }

        private void bttnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
