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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            GameInfoTable.ItemsSource = LoadCollectionData();
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
        private List<GameInfo> LoadCollectionData()
        {
            List<GameInfo> games = new List<GameInfo>();
            //adatok hozzáadása ha hozzá lesz adva az adatbázis
            return games;
        }

        private void bttnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GameInfoTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //átirányítás a játék felületére
            //NavigationService.Navigate(new Uri("kövi oldal neve", UriKind.Relative));
        }
    }
}
