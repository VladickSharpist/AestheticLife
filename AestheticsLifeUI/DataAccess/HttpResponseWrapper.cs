namespace AestheticsLifeUI.DataAccess;

public class HttpResponseWrapper<TResponse>
{
    public HttpResponseWrapper(TResponse response, bool success, HttpResponseMessage httpResponseMessage)
    {
        Success = success;
        Response = response;
        HttpResponseMessage = httpResponseMessage;
    }
    public bool Success { get; set; }
    public TResponse Response { get; set; }
    public HttpResponseMessage HttpResponseMessage { get; set; }

    public async Task<string> GetBody()
    {
        return await HttpResponseMessage.Content.ReadAsStringAsync();
    }
}