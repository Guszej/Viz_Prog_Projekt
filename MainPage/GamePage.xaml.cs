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
        public GamePage(int gameId)
        {
            InitializeComponent();
            selectedid = gameId;
            SelectedGame(selectedid);
        }
        private void SelectedGame(int szam)
        {
            var sgame = (from a in context.Games
                         where a.Id == szam
                         select a);
            foreach (var item in sgame)
            {
                GameName.Content = item.Név;
                GameDev.Content = $"Fejlesztők: {item.Készítő}";
                GameDate.Content = $"Megjelenési dátum: {item.Megjelenés}";
                GamePlatforms.Content = $"Platform: {item.Platform}";
                GameType.Content = $"Genre: {item.Típus}";
                GameMode.Content = $"Mód: {item.Mód}";
                GameRating.Content = $"Értékelés: {item.GÉrtékelés}";
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
            NavigationService.Navigate(new Uri("GameRatingWindow.xaml", UriKind.Relative));
        }
    }
}
