using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
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

            // Értékelés elrejtése vendég rang esetén
            if (bejelentkezettFelhasznalo.Név == "Vendég")
            {
                ErtekelesOszlop.Visibility = Visibility.Collapsed;
                GameDataGrid.Width = 393;
            }

            // Admin rang esetén a játék hozzáadása gomb látható
            if (bejelentkezettFelhasznalo.Rang == "Admin")
            {
                btnAddGame.Visibility = Visibility.Visible;
            }
            LoadGames();
            KepekBase64Beolvasasa();
        }

        private void LoadGames()
        {
            var gamesWithImages = (from g in _context.Games
                                   join k in _context.Képs on g.Id equals k.GameId into kepek
                                   from k in kepek.DefaultIfEmpty()
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
                                       KepUtvonal = k != null ? ConvertBase64ToImage(k.Utvonal) : null
                                   }).ToList();

            GameDataGrid.ItemsSource = gamesWithImages;
        }
        private void bttnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            var addGamePage = new AddGamePage(bejelentkezettFelhasznalo);
            addGamePage.JatekHozzaadva += (s, args) => LoadGames();
            NavigationService.Navigate(addGamePage);
        }

        //Bencék
        private void GameDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedGame = GameDataGrid.SelectedItem;
            if (selectedGame != null)
            {

                int gameId = (int)selectedGame.GetType().GetProperty("Id").GetValue(selectedGame);

                var kivi = new GamePage(gameId, bejelentkezettFelhasznalo);
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
                        KepUtvonal = g.Képs.FirstOrDefault() != null ? ConvertBase64ToImage(g.Képs.FirstOrDefault().Utvonal) : null
                    })
                    .ToList();

                GameDataGrid.ItemsSource = jatekok;
            }
        }

        private void KepekBase64Beolvasasa()
        {
            string kepekMappa = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Kepek");

            if (!Directory.Exists(kepekMappa))
            {
                Console.WriteLine("A Kepek mappa nem található.");
                return;
            }

            string[] fajlok = Directory.GetFiles(kepekMappa, "*.jpg");

            if (fajlok.Length == 0)
            {
                Console.WriteLine("Nincsenek képek a mappában.");
                return;
            }

            using (var context = new GameContext())
            {
                foreach (string fajl in fajlok)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(fajl);

                    if (int.TryParse(fileName, out int gameId))
                    {
                        var jatek = context.Games.FirstOrDefault(g => g.Id == gameId);

                        if (jatek != null)
                        {
                            try
                            {
                                byte[] imageBytes = File.ReadAllBytes(fajl);
                                string base64String = Convert.ToBase64String(imageBytes);

                                // Kép hozzáadása vagy frissítése
                                var letezoKep = context.Képs.FirstOrDefault(k => k.GameId == jatek.Id);
                                if (letezoKep == null)
                                {
                                    context.Képs.Add(new Kép
                                    {
                                        GameId = jatek.Id,
                                        Utvonal = base64String
                                    });
                                }
                                else
                                {
                                    letezoKep.Utvonal = base64String;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Hiba a {fileName} fájl feldolgozása során: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"A {fileName} fájlhoz nem található játék.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"A fájlnév nem érvényes ID: {fileName}");
                    }
                }

                context.SaveChanges();
            }
        }

        public static BitmapImage ConvertBase64ToImage(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return null;

            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageBytes))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze(); 
                return image;
            }
        }
    }
}