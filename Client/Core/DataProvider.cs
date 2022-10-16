using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Core;

internal static class DataProvider
{
    private static readonly HttpClient _client = new();

    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    internal static async Task<T> GetAsync<T>(string controller)
    {
        var requestString = Config.HOST + controller;

        var response = await _client.GetAsync(requestString);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<T>(content, _serializerOptions);

        return result;
    }

    internal static async Task<HttpStatusCode> PostAsync<T>(T value, string controller)
    {
        var requestString = Config.HOST + controller;

        var json = JsonSerializer.Serialize(value);

        var answer = await _client.PostAsync(requestString, new StringContent(json, Encoding.UTF8, "application/json"));

        return answer.StatusCode;
    }

    internal static async Task<HttpStatusCode> PutAsync<T>(T value, string controller)
    {
        var requestString = Config.HOST + controller;

        var json = JsonSerializer.Serialize(value);

        var answer = await _client.PutAsync(requestString, new StringContent(json, Encoding.UTF8, "application/json"));

        return answer.StatusCode;
    }

    internal static async Task<HttpStatusCode> DeleteAsync(Guid id, string controller)
    {
        var requestString = Config.HOST + controller + $"/{id}";

        var answer = await _client.DeleteAsync(requestString);

        return answer.StatusCode;
    }
}