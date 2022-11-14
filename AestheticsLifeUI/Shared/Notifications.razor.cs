using AestheticsLifeUI.DataAccess.Abstractions;
using AestheticsLifeUI.Extensions;
using AestheticsLifeUI.Helpers;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace AestheticsLifeUI.Shared;

public partial class Notifications
{
    [Inject]
    public IJSRuntime _js { get; set; }
    
    [Inject]
    public IHttpService _HttpService { get; set; }
    [Inject] INotificationService NotificationService { get; set; }
    
    private HubConnection? hubConnection;
    
    protected override async Task OnInitializedAsync()
    {
        var token = await _js.GetFromLocalStorage(Constants.ACCESS_TOKEN_STORAGE_KEY);
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7112/hubs/notifications",
                opt =>
                {
                    opt.AccessTokenProvider = () => Task.FromResult(token);
                    // opt.SkipNegotiation = true;
                    // opt.Transports = HttpTransportType.WebSockets;
                })
            .WithAutomaticReconnect()
            .Build();
        
        hubConnection.On<string>("SendNotificationAsync", 
            message => NotificationService.Info(message, options: options => options.IntervalBeforeClose = 3000));

        try
        {
            await hubConnection.StartAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}