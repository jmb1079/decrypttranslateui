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
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            organizations = await JsonSerializer.DeserializeAsync
                <IEnumerable<Organization>>(responseStream) ??  Enumerable.Empty<Organization>();
        }
        return organizations;
    }

    public async Task<Organization> GetOrganizationById(int id)
    {
        var organization = new Organization();
        var organizationRequest = new HttpRequestMessage(HttpMethod.Get, String.Format("/api/organization/{0}", id));
        var response = await _httpClient.SendAsync(organizationRequest);
        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            organization = await JsonSerializer.DeserializeAsync<Organization>(responseStream) ?? organization;
        }
        return organization;
    }
}
