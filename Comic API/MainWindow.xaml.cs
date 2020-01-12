using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Comic_API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int maxNumber = 0;
        private int currentNumber = 0;
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            nextImageButton.IsEnabled = false;
            todaysComicButton.IsEnabled = false;
        }

        private async Task LoadImage(int imageNumber = 0)
        {
            var comic = await ComicProcessor.LoadComic(imageNumber);
          
            if (currentNumber == 0)
            {
                comicLabel.Content = $"Todays Comic";
            }

            if (imageNumber == 0)
            {
                maxNumber = comic.Num;
            }

            currentNumber = comic.Num;
            comicLabel.Content = $"Comic : #{ currentNumber }";

            var uriSource = new Uri(comic.Img, UriKind.Absolute);
            comicImage.Source = new BitmapImage(uriSource);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadImage();
        }

        private async void previousImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber > 1)
            {
                currentNumber -= 1;
                nextImageButton.IsEnabled = true;
                todaysComicButton.IsEnabled = true;
                await LoadImage(currentNumber);

                if (currentNumber == 1)
                {
                    previousImageButton.IsEnabled = false;
                }

            }
        }

        private async void nextImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber < maxNumber)
            {
                currentNumber += 1;
                previousImageButton.IsEnabled = true;
                await LoadImage(currentNumber);

                todaysComic();
            }
        }

        private void todaysComic()
        {
            if (currentNumber == maxNumber)
            {
                comicLabel.Content = $"Todays Comic";
                nextImageButton.IsEnabled = false;
                todaysComicButton.IsEnabled = false;
                previousImageButton.IsEnabled = true;
            }
            if (currentNumber < maxNumber)
            {
                nextImageButton.IsEnabled = true;
                previousImageButton.IsEnabled = true;
                todaysComicButton.IsEnabled = true;
            }
          
        }
       
        private void sunInformationButton_Click(object sender, RoutedEventArgs e)
        {
            SunInfo sun = new SunInfo();
            sun.Show();
        }

        private void configurationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void randomButton_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            int rand = rnd.Next(1, maxNumber);  // creates a number between 1 and max
            await LoadImage(rand);

            todaysComic();

        }

        private async void todaysComicButton_Click(object sender, RoutedEventArgs e)
        {
     
            await LoadImage(0); //0 is the most up to date comic
            todaysComic();
        }
    }
}
