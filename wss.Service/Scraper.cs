using HtmlAgilityPack;
using System.Net;

namespace wss.Service
{
    public class Scraper
    {
        public static async void Run()
        {
            var html = await GetHtmlAsync();
            var data = ParseHtmlUsingHtmlAgilityPack(html);
        }
               
        private static async Task<string> GetHtmlAsync()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            var client = new HttpClient(handler);
            return await client.GetStringAsync("https://github.com/AhmedTarekHasan");
        }

        private static List<(string RepositoryName, string Description)> ParseHtmlUsingHtmlAgilityPack(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var repositories = htmlDoc
                                .DocumentNode
                                .SelectNodes("//div[@class='js-pinned-items-reorder-container']/ol/li/div/div");

            List<(string RepositoryName, string Description)> data = new();

            foreach (var repo in repositories)
            {
                var name = repo.SelectSingleNode("div/div/span/a").InnerText;
                var description = repo.SelectSingleNode("p").InnerText;
                data.Add((name, description));
            }

            return data;
        }
    }
}