using MainPage.Models;
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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void bttnReg_Click(object sender, RoutedEventArgs e)
        {
            string felhasznalonev = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string jelszo = txtPassword.Password;
            string jelszo2 = txtPassword2.Password;

            //Mezők ki vannak-e töltve
            if (string.IsNullOrEmpty(felhasznalonev) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(jelszo) ||
                string.IsNullOrEmpty(jelszo2))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!", "Hiányzó adatok", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Jelszavak eggyeznek
            if (jelszo != jelszo2)
            {
                MessageBox.Show("A jelszavak nem egyeznek!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new GameContext())
            {
                //Ellenőrzés: van-e már ilyen felhasználónév vagy email
                bool letezik = context.Felhasználós.Any(f => f.Email == email || f.Név == felhasznalonev);

                if (letezik)
                {
                    MessageBox.Show("Ez az e-mail cím vagy felhasználónév már foglalt.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                //Új felhasználó hozzáadása
                var ujFelhasznalo = new Felhasználó
                {
                    Név = felhasznalonev,
                    Email = email,
                    Jelszó = jelszo, // Lehet kell majd kódolás
                    Rang = "Alap"
                };

                context.Felhasználós.Add(ujFelhasznalo);
                context.SaveChanges();

                MessageBox.Show("Sikeres regisztráció!", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);

                // Opcionális: Navigálás másik oldalra
                NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
            }
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

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
