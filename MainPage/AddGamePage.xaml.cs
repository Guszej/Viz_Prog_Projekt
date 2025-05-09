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
    /// Interaction logic for AddGamePage.xaml
    /// </summary>
    public partial class AddGamePage : Page
    {
        private readonly GameContext _context = new GameContext();
        private readonly Felhasználó bejelentkezettFelhasznalo;

        public AddGamePage(Felhasználó felhasznalo)
        {
            InitializeComponent();
            bejelentkezettFelhasznalo = felhasznalo;

            ////if (bejelentkezettFelhasznalo.Rang != "Admin")
            ////{
            ////    MessageBox.Show("Nincs jogosultságod játékot hozzáadni!");
            ////    NavigationService.GoBack();
            ////}
        }

        public event EventHandler? JatekHozzaadva;

        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtNev.Text.Trim();

                bool jatekLetezik = _context.Games.Any(g => g.Név == name);
                if (jatekLetezik)
                {
                    MessageBox.Show("Már létezik ilyen nevű játék az adatbázisban!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var ujJatek = new Game
                {
                    Név = txtNev.Text,
                    Megjelenés = txtMegjelenes.Text,
                    Készítő = txtKeszito.Text,
                    Típus = txtTipus.Text,
                    Platform = txtPlatform.Text,
                    Mód = txtMod.Text,
                    GÉrtékelés = double.Parse(txtertek.Text),
                };
                _context.Games.Add(ujJatek);

                var log = new Logok
                {
                    FelhasználóId = bejelentkezettFelhasznalo.Id,
                    Muvelet = $"{name} hozzáadva",
                    Datum = DateTime.Now
                };
                _context.Logok.Add(log);
                _context.SaveChanges();
                JatekHozzaadva?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Játék sikeresen hozzáadva!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                string uzenet = ex.Message;
                if (ex.InnerException != null)
                    uzenet += "\n\nRészletek: " + ex.InnerException.Message;

                MessageBox.Show("Hiba történt: " + uzenet, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
