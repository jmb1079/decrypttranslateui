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
        IEnumerable<Investigator> organizations = Enumerable.Empty<Investigator>();
        var request = new HttpRequestMessage(HttpMethod.Get,"/api/investigator");
        Console.WriteLine(request.RequestUri);
        var response = await _httpClient.SendAsync(request);
        Console.WriteLine(response.IsSuccessStatusCode);
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            organizations = await JsonSerializer.DeserializeAsync
                <IEnumerable<Investigator>>(responseStream) ??  Enumerable.Empty<Investigator>();
        }
        return organizations;
    }
}
