using System.Net;
using System.Windows;
using System.Text.RegularExpressions;

namespace parserUserInfo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnFindInfo_Click(object sender, RoutedEventArgs e)
        {
            using(WebClient webClient = new WebClient())
            {
                string stringWithIP = webClient.DownloadString("http://checkip.dyndns.org/");
                string ip = new Regex(@"\d+.\d+.\d+.\d+").Match(stringWithIP).Value;
                string info = webClient.DownloadString($"http://ipwhois.app/json/{ip}");

                string continent = new Regex("\"continent\":\"(\\w+(\\s\\w+)*)\"").Match(info).Groups[1].Value;
                string country = new Regex("\"country\":\"(\\w+(\\s\\w+)*)\"").Match(info).Groups[1].Value;
                string countryCode = new Regex("\"country_code\":\"(\\w+)\"").Match(info).Groups[1].Value;
                string countryCapital = new Regex("\"country_capital\":\"(\\w+(\\s\\w+)*)\"").Match(info).Groups[1].Value;
                string countryPhone = new Regex("\"country_phone\":\"(\\+\\d+)\"").Match(info).Groups[1].Value;
                string countryNeighbours = new Regex("\"country_neighbours\":\"(\\w+(,\\w+)*)\"").Match(info).Groups[1].Value;
                string region = new Regex("\"region\":\"(\\w+(\\s\\w+)*)\"").Match(info).Groups[1].Value;
                string city = new Regex("\"city\":\"(\\w+(\\s\\w+)*)\"").Match(info).Groups[1].Value;
                string timezone = new Regex("\"timezone\":\"(\\w+\\\\\\/\\w+)\"").Match(info).Groups[1].Value;
                string timezoneGmt = new Regex("\"timezone_gmt\":\"(GMT [-+]\\d+:\\d+)\"").Match(info).Groups[1].Value;

                tbInfo.AppendText
                (
                    $"IP: {ip}\n" +
                    $"Continent: {continent}\n" +
                    $"Country: {country}\n" +
                    $"Country code: {countryCode}\n" +
                    $"Coutnry capital: {countryCapital}\n" +
                    $"Country phone: {countryPhone}\n" +
                    $"Country neighbours: {countryNeighbours}\n" +
                    $"Region: {region}\n" +
                    $"City: {city}\n" +
                    $"Timezone: {timezone}\n" +
                    $"Timezone gmt: {timezoneGmt}"
                );

                wbMap.Navigate(string.Format($"https://maps.google.com/maps/"));
            }
        }
    }
}
