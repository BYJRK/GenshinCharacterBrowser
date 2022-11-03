using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GenshinCharacterBrowser.Helpers;

public static class HttpHelper
{
    private static HttpClient httpClient = new();

    /// <summary>
    /// Get an http response from sending request to a specific url
    /// </summary>
    /// <param name="url"></param>
    /// <param name="method">Http method being used (default: <see cref="HttpMethod.Get"/>)</param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> GetResponseAsync(string url, HttpMethod method = null)
    {
        using var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = method ?? HttpMethod.Get
        };
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return response;
    }

    /// <summary>
    /// Get string content from a specific url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<string> GetStringAsync(string url)
    {
        using var response = await GetResponseAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Get a bitmap image from a specific url
    /// </summary>
    public static async Task<BitmapImage> GetImageAsync(string url)
    {
        using var response = await GetResponseAsync(url);
        var data = await response.Content.ReadAsByteArrayAsync();
        using var stream = new MemoryStream(data);

        var image = new BitmapImage();
        image.BeginInit();
        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.UriSource = null;
        image.StreamSource = stream;
        image.EndInit();
        image.Freeze();

        return image;
    }
}
