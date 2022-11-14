using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AestheticsLifeUI.DataAccess.Abstractions;
using AestheticsLifeUI.Services.Abstractions;

namespace AestheticsLifeUI.DataAccess;

public class HttpService: IHttpService
{
    public HttpClient HttpClient { get;}
    private JsonSerializerOptions defaultOptions => new() { PropertyNameCaseInsensitive = true };
    
    public HttpService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public async Task<HttpResponseWrapper<TResponse>> GetAsync<TResponse>(string url)
    {
        var responseHttp = await HttpClient.GetAsync(url);

        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await Deserialize<TResponse>(responseHttp, defaultOptions);
            return new HttpResponseWrapper<TResponse>(response, true, responseHttp); 
        }

        return new HttpResponseWrapper<TResponse>(default, false, responseHttp);
    }

    public async Task<HttpResponseWrapper<TResponse>> PostAsync<TBody, TResponse>(string url, TBody data)
    {
        var dataJson = JsonSerializer.Serialize(data);
        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
        var response = await HttpClient.PostAsync(url, stringContent);
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<TResponse>(response, defaultOptions);
            return new HttpResponseWrapper<TResponse>(responseDeserialized, true, response);
        }
        return new HttpResponseWrapper<TResponse>(default, false, response);
    }
    
    public async Task<HttpResponseWrapper<TResponse>> PutAsync<TBody, TResponse>(string url, TBody data)
    {
        var dataJson = JsonSerializer.Serialize(data);
        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
        var response = await HttpClient.PutAsync(url, stringContent);
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<TResponse>(response, defaultOptions);
            return new HttpResponseWrapper<TResponse>(responseDeserialized, true, response);
        }
        return new HttpResponseWrapper<TResponse>(default, false, response);
    }
    
    public async Task<HttpResponseWrapper<bool>> DeleteAsync(string url)
    {
        var responseHttp = await HttpClient.DeleteAsync(url);

        if (responseHttp.IsSuccessStatusCode)
        {
            return new HttpResponseWrapper<bool>(true, true, responseHttp);
        }

        return new HttpResponseWrapper<bool>(false, false, responseHttp);
    }
    
    public string BuildUrl(string controller , string? action, string? serviceName, long? id, params QueryParam[] queryParams)
    {
        var url = new StringBuilder(HttpClient.BaseAddress.ToString());
        if (!string.IsNullOrEmpty(serviceName))
        {
            url.Append($"{serviceName}");
            url.Append($"/{controller}");
        }
        else
        {
            url.Append($"{controller}");
        }

        if (!string.IsNullOrEmpty(action))
        {
            url.Append($"/{action}");
        }

        if (id != null)
        {
            url.Append($"/{id}");
        }

        if (queryParams.Length > 0)
        {
            url.Append("?");
            foreach (var param in queryParams)
            {
                url.Append(param.ToUrlParam());
            }
        }

        return url.ToString();
    }

    public string BuildUrl(string controller, string action)
    {
        return BuildUrl(controller, action, String.Empty, null);
    }
    
    public string BuildUrl(string controller, string action, string serviceName)
    {
        return BuildUrl(controller, action, serviceName, null);
    }
    
    public string BuildUrl(string controller, string action, long id)
    {
        return BuildUrl(controller, action, String.Empty, id);
    }
    
    private async Task<T> Deserialize<T>(HttpResponseMessage responseMessage, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
    }
}