using System.IO;
using System.Text;
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

namespace MainPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
            
            MainFrame.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
        }
        Image CreateBitmap(string uri)
        {
            return new Image() { Source = new BitmapImage(new Uri(uri)) };
        }
        Image[] GetImages()
        {
            var imageUris = new[]
            {
                "C:\\Users\\User\\Viz_Prog_Projekt\\MainPage\\Images\\kep.png",
                "C:\\Users\\User\\Viz_Prog_Projekt\\MainPage\\Images\\backgrndimg.png"
            };
            return imageUris.Select(CreateBitmap).ToArray();
        }
        public void SetBackground()
        {
            for (int i = 0; i < GetImages().Length; i++)
            {
                Image bimage = GetImages()[i];
                bImage.Source = bimage.Source;
                Thread.Sleep(10);
                if (i == GetImages().Length)
                    i = 0;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bImage.Source = GetImages()[0].Source;
            SetBackground();
        }


    }
}