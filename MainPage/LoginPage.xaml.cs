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

namespace MainPage
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        // Handle username text change (optional)
        private void lgUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            // You can add validation here if needed
        }

        // Handle password text change (optional)
        private void lgPWD_TextChanged(object sender, RoutedEventArgs e)
        {
            // You can add validation here if needed
        }

        // Login button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = lgUser.Text;
            string password = lgPWD.Password; // Using PasswordBox for security

            if (username == "admin" && password == "1234") // Example credentials
            {
                //MessageBox.Show("Sikeres bejelentkezés!", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new Uri("Main.xaml", UriKind.Relative));
                // Navigate to another page if needed
                // NavigationService.Navigate(new Uri("Dashboard.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Back button click
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void bttnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
