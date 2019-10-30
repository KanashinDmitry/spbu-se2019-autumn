using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task04
{
    class Program
    {
        static async Task Main()
        {
            string mainUrl = Console.ReadLine();
            
            var webData = await DownloadDataAsync(mainUrl);
            
            MatchCollection matches = GetUrls(webData);
            List<string> urls = new List<string>();
            
            if (matches.Count > 0)
            {
                foreach (var match in matches)
                {
                    var item = match.ToString();
                    if (!urls.Contains(item))
                    {
                        urls.Add(match.ToString()); 
                    }
                }     
            }
            else
            {
                Console.WriteLine("No matches found");
                Environment.Exit(1);
            }
            
            await ReadUrlsAsync(urls.Select(str => new Regex(@"https?://\S*""")
                                                .Match(str)
                                                .ToString().TrimEnd('"')));
        }

        static async Task ReadUrlsAsync(IEnumerable<string> newUrls)
        {
            var tasks = new List<Task>();
            foreach (string url in newUrls)
            {
                tasks.Add(Task.Run(async () =>
                {
                    string data = await DownloadDataAsync(url);
                    long length = data.Length;
                    if (data != "") Console.WriteLine($"URL: {url}  Length: {length}");
                }));
            }

            await Task.WhenAll(tasks);
        }
        static async Task<string> DownloadDataAsync(string url)
        {
            var webData = "";

            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = await GetResponseAsync(request);
                
                using Stream stream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(stream);
                
                webData = await reader.ReadToEndAsync();

                response.Close();
            }
            catch (WebException e)
            {
                Console.WriteLine($"URL: {url} Cannot response from url");
            }
            catch (System.UriFormatException e)
            {
                Console.WriteLine(e.Message);
            }

            return webData;
        }

        static async Task<WebResponse> GetResponseAsync(WebRequest request)
        {
            return await Task.Run(request.GetResponse);
        }
        
        static MatchCollection GetUrls(string webData)
        {
            const string pattern = @"<a([^>]*)href=""https?://.*"">";
            Regex regex = new Regex(pattern);
            return regex.Matches(webData);
        }
    }
}