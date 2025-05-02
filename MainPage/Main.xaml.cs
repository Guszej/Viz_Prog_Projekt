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
using Microsoft.EntityFrameworkCore;

namespace MainPage
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        private readonly GameContext _context = new GameContext();
        public Main()
        {
            InitializeComponent();
            
            LoadGames();
        }

        private void LoadGames()
        {
            var gamesWithImages = (from g in _context.Games
                                   join k in _context.Képs on g.Id equals k.GameId
                                   select new
                                   {
                                       g.Id,
                                       g.Név,
                                       g.Megjelenés,
                                       g.Készítő,
                                       g.Típus,
                                       g.Platform,
                                       g.Mód,
                                       g.GÉrtékelés,
                                       g.Értékelés,
                                       KepUtvonal = k.Utvonal
                                   }).ToList();

            GameDataGrid.ItemsSource = gamesWithImages;
        }

        public class GameInfo
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Dev { get; set; }
            public string ReleaseDate {  get; set; }
            public string Type { get; set; }
            public string Platform { get; set; }
            public string Mode {  get; set; }
            public float Rating {  get; set; }
        }
       

        private void bttnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GameInfoTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //átirányítás a játék felületére
            NavigationService.Navigate(new Uri("GamePage.xaml", UriKind.Relative));
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
