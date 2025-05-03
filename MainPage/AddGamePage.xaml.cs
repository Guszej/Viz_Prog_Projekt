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

            if (bejelentkezettFelhasznalo.Rang != "Admin")
            {
                MessageBox.Show("Nincs jogosultságod játékot hozzáadni!");
                NavigationService.GoBack();
            }
        }

        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                _context.SaveChanges();

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

        private void Vissza_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
