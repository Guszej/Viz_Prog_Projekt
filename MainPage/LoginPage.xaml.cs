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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }


        private void lgUser_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lgPWD_TextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string user = lgUser.Text.Trim();
            string jelszo = lgPWD.Password;

            using (var context = new GameContext())
            {
                // 1. Felhasználó lekérdezése felhasználónév vagy e-mail alapján
                var felhasznalo = context.Felhasználós
                    .FirstOrDefault(f => (f.Név == user || f.Email == user));

                if (felhasznalo == null)
                {
                    MessageBox.Show("Nincs ilyen felhasználó vagy e-mail cím.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 2. Jelszó ellenőrzés
                if (felhasznalo.Jelszó.Trim() != jelszo)
                {
                    MessageBox.Show("Hibás jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 3. Rang lekérdezés
                string rang = felhasznalo.Rang;

                MessageBox.Show($"Sikeres bejelentkezés! Rang: {rang}", "Bejelentkezés", MessageBoxButton.OK, MessageBoxImage.Information);

                // 4. Átadás a MainPage-re, a felhasználó adataival, bejelentkezés logolása
                var main = new Main(felhasznalo);
                var log = new Logok
                {
                    FelhasználóId = felhasznalo.Id,
                    Muvelet = "Bejelentkezés",
                    Datum = DateTime.Now
                };

                context.Logok.Add(log);
                context.SaveChanges();
                this.NavigationService?.Navigate(main);
            }
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
