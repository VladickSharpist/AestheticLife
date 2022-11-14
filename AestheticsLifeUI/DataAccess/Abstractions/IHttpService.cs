using AestheticsLifeUI.Services.Abstractions;

namespace AestheticsLifeUI.DataAccess.Abstractions;

public interface IHttpService
{
    public HttpClient HttpClient { get;}
    Task<HttpResponseWrapper<TResponse>> PostAsync<TBody, TResponse>(string url, TBody data);
    Task<HttpResponseWrapper<TResponse>> GetAsync<TResponse>(string url);
    Task<HttpResponseWrapper<TResponse>> PutAsync<TBody, TResponse>(string url, TBody data);
    Task<HttpResponseWrapper<bool>> DeleteAsync(string url);

    string BuildUrl(string controller, string action, long id);
    string BuildUrl(string controller, string action, string serviceName);
    string BuildUrl(string controller, string action);
    string BuildUrl(string controller, string? action, string? serviceName, long? id, params QueryParam[] queryParams);
}