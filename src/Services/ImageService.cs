using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace DecryptTranslateUi.Data;

public class ImageService
{
    private readonly HttpClient _httpClient;
    private IConfiguration _configuration;

    public ImageService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://dtlawapi.azurewebsites.net");
    }

    public async Task<IEnumerable<Image>> GetAllImagesAsync()
    {
        IEnumerable<Image> images = Enumerable.Empty<Image>();
        var request = new HttpRequestMessage(HttpMethod.Get,"/api/image");
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            images = await JsonSerializer.DeserializeAsync
                <IEnumerable<Image>>(responseStream) ??  Enumerable.Empty<Image>();
        }
        return images;
    }

    public async Task<Image> GetImageById(int id)
    {
        var image = new Image();
        var imageRequest = new HttpRequestMessage(HttpMethod.Get, String.Format("/api/image/{0}", id));
        var response = await _httpClient.SendAsync(imageRequest);
        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            image = await JsonSerializer.DeserializeAsync<Image>(responseStream) ?? image;
        }
        return image;
    }

    public async Task<HttpResponseMessage> UploadImage(ImageUploadDto imageUpload)
    {
        var imageUploadRequest = new HttpRequestMessage(HttpMethod.Post, String.Format("/api/image/blob"));
        imageUploadRequest.Content = new StringContent(JsonSerializer.Serialize(imageUpload), Encoding.UTF8, "application/json");
        Console.WriteLine(imageUploadRequest.Content);
        var response = await _httpClient.SendAsync(imageUploadRequest);
        return response;
    }
}
