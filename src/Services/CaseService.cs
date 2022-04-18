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
        //_httpClient.BaseAddress = new Uri("https://decrypttranslateapi.azurewebsites.net/api/case");
    }

    public async Task<IEnumerable<Case>> GetAllCasesAsync()
    {
        IEnumerable<Case> cases = Enumerable.Empty<Case>();
        var request = new HttpRequestMessage(HttpMethod.Get,"https://decrypttranslateapi.azurewebsites.net/api/case");
        Console.WriteLine(request.RequestUri);
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            cases = await JsonSerializer.DeserializeAsync
                <IEnumerable<Case>>(responseStream) ??  Enumerable.Empty<Case>();
        }
        return cases;
    }
}
