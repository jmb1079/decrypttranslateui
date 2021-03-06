using System.Text;
using System.Text.Json;
using DecryptTranslateUi.Data;

namespace DecryptTranslateUi.Data;

public class CaseService
{
    private readonly HttpClient _httpClient;
    private IConfiguration configuration;

    public CaseService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://dtlawapi.azurewebsites.net");
    }

    public async Task<IEnumerable<Case>> GetAllCasesAsync()
    {
        IEnumerable<Case> cases = Enumerable.Empty<Case>();
        var request = new HttpRequestMessage(HttpMethod.Get,"/api/case");
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            cases = await JsonSerializer.DeserializeAsync
                <IEnumerable<Case>>(responseStream) ??  Enumerable.Empty<Case>();
        }
        return cases;
    }

    public async Task<HttpResponseMessage> CreateCase(Case thisCase)
    {
        var existingCases = await GetAllCasesAsync();
        var newCaseRequest = new HttpRequestMessage(HttpMethod.Post, "/api/case");
        newCaseRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        newCaseRequest.Content = new StringContent(JsonSerializer.Serialize(thisCase), Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(newCaseRequest);
        return response;
    }

    public async Task<Case> GetCaseByNumber(int number)
    {
        var thisCase = new Case();
        var caseRequest = new HttpRequestMessage(HttpMethod.Get, String.Format("/api/case/{0}", number));
        var response = await _httpClient.SendAsync(caseRequest);
        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            thisCase = await JsonSerializer.DeserializeAsync<Case>(responseStream) ?? thisCase;
        }
        return thisCase;
    }

}
