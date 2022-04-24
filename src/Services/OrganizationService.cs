using System.Text.Json;
using System.Net.Http;

namespace DecryptTranslateUi.Data;

public class OrganizationService
{
    private readonly HttpClient _httpClient;
    private IConfiguration _configuration;

    public OrganizationService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://dtlawapi.azurewebsites.net");
    }

    public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync()
    {
        IEnumerable<Organization> organizations = Enumerable.Empty<Organization>();
        var request = new HttpRequestMessage(HttpMethod.Get,"/api/organization");
        Console.WriteLine(request.RequestUri);
        var response = await _httpClient.SendAsync(request);
        Console.WriteLine(response.IsSuccessStatusCode);
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            organizations = await JsonSerializer.DeserializeAsync
                <IEnumerable<Organization>>(responseStream) ??  Enumerable.Empty<Organization>();
        }
        return organizations;
    }
}
