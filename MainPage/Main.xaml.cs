using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MainPage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MainPage
{
    public partial class Main : Page
    {
        private readonly GameContext _context = new GameContext();
        private readonly Felhasználó bejelentkezettFelhasznalo;

        public Main(Felhasználó felhasznalo)
        {
            InitializeComponent();
            bejelentkezettFelhasznalo = felhasznalo;

            // Értékelés oszlop elrejtése vendég esetén
            if (bejelentkezettFelhasznalo.Név == "Vendég")
            {
                ErtekelesOszlop.Visibility = Visibility.Collapsed;
                GameDataGrid.Width = 393;
            }

            if (bejelentkezettFelhasznalo.Rang == "Admin")
            {
                btnAddGame.Visibility = Visibility.Visible;
            }
            LoadGames();
            GameDataGrid.CellEditEnding += GameDataGrid_CellEditEnding;
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
                                       Értékelés = _context.Értékelés
                                           .Where(e => e.GameId == g.Id && e.FelhasználóId == bejelentkezettFelhasznalo.Id)
                                           .Select(e => e.FelhasználóÉrtékelés)
                                           .FirstOrDefault(),
                                       KepUtvonal = k.Utvonal
                                   }).ToList();

            GameDataGrid.ItemsSource = gamesWithImages;
        }

        private void GameDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Értékelés")
            {
                var row = (dynamic)e.Row.Item;
                var textbox = e.EditingElement as TextBox;
                if (textbox != null && double.TryParse(textbox.Text, out double ujErtekeles))
                {
                    var gameId = (int)row.Id;

                    if (ujErtekeles < 0 || ujErtekeles > 10)
                    {
                        MessageBox.Show("Adj meg egy számot 0 és 10 között!");
                        return;
                    }

                    var letezo = _context.Értékelés.FirstOrDefault(x =>
                        x.FelhasználóId == bejelentkezettFelhasznalo.Id &&
                        x.GameId == gameId);

                    if (letezo != null)
                    {
                        letezo.FelhasználóÉrtékelés = ujErtekeles;
                    }
                    else
                    {
                        var uj = new Értékelé
                        {
                            FelhasználóId = bejelentkezettFelhasznalo.Id,
                            GameId = gameId,
                            FelhasználóÉrtékelés = ujErtekeles
                        };
                        _context.Értékelés.Add(uj);
                    }

                    // GÉrtékelés frissítése
                    var ertekelesek = _context.Értékelés
                        .Where(x => x.GameId == gameId)
                        .ToList();

                    var game = _context.Games.FirstOrDefault(x => x.Id == gameId);
                    if (game != null && ertekelesek.Count > 0)
                    {
                        game.GÉrtékelés = (float)ertekelesek.Average(x => x.FelhasználóÉrtékelés);
                    }

                    _context.SaveChanges();
                    MessageBox.Show("Értékelés mentve!");
                    LoadGames(); // Frissítjük a táblázatot
                }
            }
        }

        private void bttnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddGamePage(bejelentkezettFelhasznalo));
        }
        private void GameDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedGame = GameDataGrid.SelectedItem;
            if (selectedGame != null)
            {
                // Dinamikus ID lekérés
                int gameId = (int)selectedGame.GetType().GetProperty("Id").GetValue(selectedGame);

                var kivi = new GamePage(gameId);
                this.NavigationService?.Navigate(kivi);
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void KeresesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keresettSzoveg = KeresesTextBox.Text?.Trim().ToLower() ?? "";

            using (var context = new GameContext())
            {
                IQueryable<Game> query = context.Games.Include(g => g.Képs);

                if (!string.IsNullOrEmpty(keresettSzoveg))
                {
                    query = query.Where(g => g.Név.ToLower().Contains(keresettSzoveg));
                }

                var jatekok = query
                    .Select(g => new
                    {
                        Id = g.Id,
                        Név = g.Név,
                        Megjelenés = g.Megjelenés,
                        Készítő = g.Készítő,
                        Típus = g.Típus,
                        Platform = g.Platform,
                        Mód = g.Mód,
                        GÉrtékelés = g.GÉrtékelés,
                        Értékelés = context.Értékelés
                            .Where(e => e.GameId == g.Id && e.FelhasználóId == bejelentkezettFelhasznalo.Id)
                            .Select(e => e.FelhasználóÉrtékelés)
                            .FirstOrDefault(),
                        KepUtvonal = g.Képs.FirstOrDefault().Utvonal ?? string.Empty
                    })
                    .ToList();

                GameDataGrid.ItemsSource = jatekok;
            }
        }

    }
}