using System.Text.Json;
using System.Net.Http;

namespace DecryptTranslateUi.Data;

public class InvestigatorService
{
    private readonly HttpClient _httpClient;
    private IConfiguration _configuration;

    public InvestigatorService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://dtlawapi.azurewebsites.net");
    }

    public async Task<IEnumerable<Investigator>> GetAllInvestigatorsAsync()
    {
        IEnumerable<Investigator> investigators = Enumerable.Empty<Investigator>();
        var request = new HttpRequestMessage(HttpMethod.Get,"/api/investigator");
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            investigators = await JsonSerializer.DeserializeAsync
                <IEnumerable<Investigator>>(responseStream) ??  Enumerable.Empty<Investigator>();
        }
        return investigators;
    }

    public async Task<Investigator> GetInvestigatorById(string id)
    {
        var investigator = new Investigator();
        var investigatorRequest = new HttpRequestMessage(HttpMethod.Get, String.Format("/api/investigator/{0}", id));
        var response = await _httpClient.SendAsync(investigatorRequest);
        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            investigator = await JsonSerializer.DeserializeAsync<Investigator>(responseStream) ?? investigator;
        }
        return investigator;
    }
}
