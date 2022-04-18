using System.Text.Json;

namespace DecryptTranslateUi.Data;

public class OrganizationService
{
    private readonly HttpClient _httpClient;
    private IConfiguration configuration;

    public OrganizationService()
    {
        _httpClient = new HttpClient();
        //_httpClient.BaseAddress = new Uri("https://decrypttranslateapi.azurewebsites.net/api/organization");
    }

    public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync()
    {
        IEnumerable<Organization> organizations = Enumerable.Empty<Organization>();
        var request = new HttpRequestMessage(HttpMethod.Get,"https://decrypttranslateapi.azurewebsites.net/api/organization");
        Console.WriteLine(request.RequestUri);
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            organizations = await JsonSerializer.DeserializeAsync
                <IEnumerable<Organization>>(responseStream) ??  Enumerable.Empty<Organization>();
        }
        return organizations;
    }
}
