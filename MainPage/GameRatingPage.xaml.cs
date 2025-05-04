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
    /// Interaction logic for GameRatingPage.xaml
    /// </summary>
    public partial class GameRatingPage : Page
    {
        private readonly Felhasználó felhasznalo;
        private readonly int gameId;
        private readonly GameContext context = new GameContext();
        public GameRatingPage(int gameId, Felhasználó felhasznalo)
        {
            InitializeComponent();
            this.felhasznalo = felhasznalo;
            this.gameId = gameId;
        }

        private void GameRatingbtn_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(rtxtbxRating.Document.ContentStart, rtxtbxRating.Document.ContentEnd);
            string userRating = textRange.Text.Trim();

            if (string.IsNullOrWhiteSpace(userRating))
            {
                MessageBox.Show("Kérjük, adjon meg egy értékelést.");
                return;
            }

            var existingRating = context.Értékelés.FirstOrDefault(e =>
                e.FelhasználóId == felhasznalo.Id && e.GameId == gameId);

            if (existingRating != null)
            {
                existingRating.FelhasználóÉrtékelés = userRating;
            }
            else
            {
                var newRating = new Értékelé
                {
                    FelhasználóId = felhasznalo.Id,
                    GameId = gameId,
                    FelhasználóÉrtékelés = userRating
                };
                context.Értékelés.Add(newRating);
            }

            context.SaveChanges();
            MessageBox.Show("Értékelés elmentve!");
            NavigationService.GoBack();
        }
    }
}
