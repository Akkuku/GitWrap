using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using GitWrap.Domain.Exceptions;

namespace GitWrap.Infrastructure.Extensions;

internal static class HttpClientExtensions
{
    private static readonly JsonSerializerOptions DefaultSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public static async Task<TResponse> PostJsonAsync<TResponse>(this HttpClient httpClient, string endpoint, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        var response = await httpClient.PostAsync(endpoint, content: null);

        await EnsureSuccessStatusCode(response);

        var responseString = await response.Content.ReadAsStringAsync();

        var serializerOptions = jsonSerializerOptions ?? DefaultSerializerOptions;
        return JsonSerializer.Deserialize<TResponse>(responseString, serializerOptions)
               ?? throw new HttpResponseException("Failed to deserialize response");
    }

    public static async Task<TResponse> PostJsonAsync<TResponse, TRequest>(this HttpClient httpClient, string endpoint,
        TRequest? content = default, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        var response = await httpClient.PostAsJsonAsync(endpoint, content);

        await EnsureSuccessStatusCode(response);

        var responseString = await response.Content.ReadAsStringAsync();
        
        var serializerOptions = jsonSerializerOptions ?? DefaultSerializerOptions;
        return JsonSerializer.Deserialize<TResponse>(responseString, serializerOptions)
               ?? throw new HttpResponseException("Failed to deserialize response");
    }

    public static async Task<TResponse> PutJsonAsync<TResponse>(this HttpClient httpClient, string endpoint, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        var response = await httpClient.PutAsync(endpoint, content: null);

        await EnsureSuccessStatusCode(response);

        var responseString = await response.Content.ReadAsStringAsync();

        var serializerOptions = jsonSerializerOptions ?? DefaultSerializerOptions;
        return JsonSerializer.Deserialize<TResponse>(responseString, serializerOptions)
               ?? throw new HttpResponseException("Failed to deserialize response");
    }
    
    public static async Task<TResponse> PatchJsonAsync<TResponse, TRequest>(this HttpClient httpClient, string endpoint,
        TRequest? content = default, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        var response = await httpClient.PatchAsJsonAsync(endpoint, content);

        await EnsureSuccessStatusCode(response);

        var responseString = await response.Content.ReadAsStringAsync();
        
        var serializerOptions = jsonSerializerOptions ?? DefaultSerializerOptions;
        return JsonSerializer.Deserialize<TResponse>(responseString, serializerOptions)
               ?? throw new HttpResponseException("Failed to deserialize response");
    }

    private static async Task EnsureSuccessStatusCode(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        var message = await response.Content.ReadAsStringAsync();

        throw new HttpResponseException(
            $"Received non success status code ({response.StatusCode}) while sending request to '{response.RequestMessage?.RequestUri}': {message}",
            response.StatusCode);
    }
}