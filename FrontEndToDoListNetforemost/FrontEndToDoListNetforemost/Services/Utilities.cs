using System.Reflection;

namespace FrontEndToDoListNetforemost.Services
{
    public static class Utilities
    {
        public static string ServerUrl { get; set; } = "https://localhost:7159/";
        public static HttpClient _httpClient { get; set; } = new HttpClient();
        public static string SystemPath { get; set; } = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
    }
}
