using System.Net.Http.Headers;
using EventHubPackage.Core;

namespace WebApi.Core;

public class ServiceHttpClient
{
    private readonly HttpClient _httpClient;

    public ServiceHttpClient(string path)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = MyEnvironment.GetServiceUrl(path)
        };
        _addToken();
    }

    private void _addToken()
    {
        var token = AuthenticationExtension.GenerateToken(MyEnvironment.ServiceId, "Service");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<HttpResponseMessage> GetAsync(string path)
    {
        return await _httpClient.GetAsync(path);
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string path, T body)
    {
        return await _httpClient.PostAsJsonAsync(path, body);
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string path, T body)
    {
        return await _httpClient.PutAsJsonAsync(path, body);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string path)
    {
        return await _httpClient.DeleteAsync(path);
    }
}