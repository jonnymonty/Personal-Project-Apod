using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Apod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// set the parameter of system
        /// </summary>
        /// <param name="uAction"></param>
        /// <param name="uParam"></param>
        /// <param name="lpvParam"></param>
        /// <param name="fuWinIni"></param>
        /// <example></example>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(UAction uAction, int uParam, StringBuilder lpvParam, int fuWinIni);

        public enum UAction
        {
            /// <summary>
            /// set the desktop background image
            /// </summary>
            SPI_SETDESKWALLPAPER = 0x0014,
            /// <summary>
            /// set the desktop background image
            /// </summary>
            SPI_GETDESKWALLPAPER = 0x0073,
        }

        public ImageContext Context { get; }

        private readonly HttpClient httpClient;

        /// <summary>
        /// Regex used to get the image source
        /// </summary>
        private readonly Regex imgRegex = new Regex("<br>\\s+<a href=\"(?<link>.*?)\">\\s<img src=\"(?<src>.*?)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Regex used to get the image title
        /// </summary>
        private readonly Regex titleRegex = new Regex("<b>(?<title>.*?)</b>\\s?<br>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The base url used to get the apod image
        /// </summary>
        private const string BaseUrl = "https://apod.nasa.gov/apod/";

        /// <summary>
        /// The initial sub url
        /// </summary>
        private string subUrl = "astropix.html";

        private string filePath = string.Empty;

        /// <summary>
        /// The currently selected Date
        /// </summary>
        private DateTime selectedDate;

        public MainWindow()
        {
            this.httpClient = new HttpClient();
            this.Context = new ImageContext
            {
                Title = "Loading..."
            };

            InitializeComponent();

            selectedDate = DateTime.Today;

            this.DatePicker.Text = selectedDate.ToString();

            this.DatePicker.DisplayDateEnd = DateTime.Today;
            this.DatePicker.DisplayDateStart = new DateTime(1995, 06, 16);

            this.DataContext = this;
        }

        /// <summary>
        /// Checks to see if you have an internet connection
        /// </summary>
        /// <returns>true if you have internet and false if you do not</returns>
        private bool CheckConnection()
        {
            WebClient client = new WebClient();
            try
            {
                using (client.OpenRead("http://www.google.com"))
                {
                }
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }

        private async void LoadNewImageAsync()
        {
            this.Context.Title = "Loading...";
            this.Context.Status = "";
            this.Context.ImageSource = null;

            await this.GetLatestImageAsync();
        }

        /// <summary>
        /// Gets the latest image from apod
        /// </summary>
        /// <returns>The image context</returns>
        private async Task GetLatestImageAsync()
        {
            if (!CheckConnection())
            {
                this.Context.Title = "Please Check Your Internet Connection";
                return;
            }

            this.SetBackground.IsEnabled = false;
            this.Context.ValidImage = false;
            this.Context.Status = String.Empty;
            var html = await this.GetHtmlAsync();
            var matches = this.imgRegex.Match(html);

            var imageUrl = matches.Groups["link"]?.Value ?? matches.Groups["src"]?.Value;

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                this.Context.Title = "Error";
                this.Context.Status = "Unable to get the image, the apod may be a video";

                return;
            }

            var imageData = await this.httpClient.GetAsync(BaseUrl + imageUrl);
            if (!imageData.IsSuccessStatusCode)
            {
                this.Context.Title = "Error";
                this.Context.Status = "Unable to download latest image. status code: " + imageData.StatusCode;
                MessageBox.Show("Unable to download latest image. status code: " + imageData.StatusCode);

                return;
            }

            var stream = await imageData.Content.ReadAsStreamAsync();
            try
            {
                this.Context.ImageSource = BitmapFrame.Create(stream, BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.OnLoad);
                this.Context.FileName = imageUrl.Split('/').Last();

                var titleMatch = this.titleRegex.Match(html).Groups["title"]?.Value;

                this.Context.Title = titleMatch ?? this.Context.FileName;

                this.Context.ValidImage = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error creating image context - this might be a video.\n" + e.Message);
                this.Context.Status = "Error creating image context";
            }
        }

        /// <summary>
        /// Gets the html from the requested url
        /// </summary>
        /// <returns>The html</returns>
        private async Task<string> GetHtmlAsync()
        {
            var response = await this.httpClient.GetAsync(BaseUrl + subUrl);
            if (!response.IsSuccessStatusCode)
            {
                this.Context.Title = "Error";
                MessageBox.Show("Unable to open apod homepage. status code: " + response.StatusCode);

                return string.Empty;
            }

                var html = await response.Content.ReadAsStringAsync();
                return html;
        }

        private async void MainWindow_OnInitialized(object sender, EventArgs e)
        {
            await this.GetLatestImageAsync();
        }

        /// <summary>
        /// Button that saves the image to the designated file path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveImageAsync(object sender, RoutedEventArgs e)
        {
            if (!this.Context.ValidImage)
            {
                MessageBox.Show("Unable to save, no valid image was downloaded");

                return;
            }

            this.Context.Status = "Saving...";

            this.filePath = string.Empty;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Png (*.png)|*.png|Jpg (*.jpg)|*.jpg";
            saveFileDialog.DefaultExt = ".png";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        this.filePath = saveFileDialog.FileName;
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(this.Context.ImageSource));
                        encoder.Save(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to save image: " + ex.Message);
                    this.Context.Status = string.Empty;
                }

                this.Context.Status = "Saved to: " + this.filePath;
                this.SetBackground.IsEnabled = true;
            }
            else
            {
                this.Context.Status = string.Empty;
            }
        }

        /// <summary>
        /// Button that sets your desktop background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SetBackgroundAsync(object sender, RoutedEventArgs e)
        {
            if (!this.Context.ValidImage)
            {
                MessageBox.Show("Unable to set wallpaper, no valid image was found");

                return;
            }

            this.Context.Status = "Setting Wallpaper...";

            SetDesktopWallpaper();

        }

        private void SetDesktopWallpaper()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    StringBuilder s = new StringBuilder(filePath);
                    SystemParametersInfo(UAction.SPI_SETDESKWALLPAPER, 300, s, 0);
                    this.Context.Status = "Desktop wallpaper set";
                }
                catch (Exception ex)
                {
                    this.Context.Status = "Unable to set wallpaper: " + ex.Message;
                }
            }
            else
            {
                this.Context.Status = "Unable to find file";
            }
        }

        private async void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            // If the Date from the date picker is not equal to the current selected date
            if (DatePicker.SelectedDate != this.selectedDate)
            {
                // Set the selected date to the newly selected date
                this.selectedDate = DatePicker.SelectedDate.Value;

                // Convert the selected date to the url format
                string year = this.selectedDate.Year.ToString();
                string subyear = year.Remove(0, 2);

                string month = this.selectedDate.Month.ToString();
                string submonth = month;
                if (month.Length == 1)
                {
                    submonth = "0" + month;
                }

                string day = this.selectedDate.Day.ToString();
                string subday = day;
                if (day.Length == 1)
                {
                    subday = "0" + day;
                }

                subUrl = "ap" + subyear + submonth + subday + ".html";

                LoadNewImageAsync();
            }
        }
    }
}
