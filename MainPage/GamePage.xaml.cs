using System;
using System.Collections.Generic;
using System.IO.Pipelines;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;

namespace MainPage
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private GameContext context = new GameContext();
        private int selectedid;
        private readonly Felhasználó bejelentkezettFelhasznalo;
        public GamePage(int gameId, Felhasználó felhasznalo)
        {
            InitializeComponent();
            selectedid = gameId;
            SelectedGame(selectedid);
            bejelentkezettFelhasznalo = felhasznalo;
            if (bejelentkezettFelhasznalo.Név == "Vendég")
            {
                btnRating.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnRating.Visibility = Visibility.Visible;
            }

        }
        private void SelectedGame(int szam)
        {
            var sgame = (from a in context.Games
                         where a.Id == szam
                         select a);
            var skep = (from a in context.Képs
                        where a.GameId == szam
                        select a.Utvonal).FirstOrDefault();
            ImageSource image = new BitmapImage(new Uri(skep));
            foreach (var item in sgame)
            {
                GameImg.Source = image;
                GameName.Text = item.Név;
                GameDev.Text = $"Fejlesztők: {item.Készítő}";
                GameDate.Text = $"Megjelenési dátum: {item.Megjelenés}";
                GamePlatforms.Text = $"Platform: {item.Platform}";
                GameType.Text = $"Genre: {item.Típus}";
                GameMode.Text = $"Mód: {item.Mód}";
                GameRating.Text = $"Értékelés: {item.GÉrtékelés}";
            }
        }
        private void bttnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();          
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SelectedGame(selectedid);
        }

        private void btnRating_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameRatingPage(selectedid, bejelentkezettFelhasznalo));
        }
    }
}
