using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using AestheticsLifeUI.DataAccess.Models.Login;
using AestheticsLifeUI.Extensions;
using AestheticsLifeUI.Services.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace AestheticsLifeUI.Helpers;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
{
    private readonly IJSRuntime _js;
    private readonly HttpClient _client;
    private AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

    public JwtAuthenticationStateProvider(IJSRuntime js, HttpClient client)
    {
        _js = js;
        _client = client;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _js.GetFromLocalStorage(Constants.ACCESS_TOKEN_STORAGE_KEY);
        if (string.IsNullOrEmpty(token))
        {
            return Anonymous;
        }
        var accessExpirationTime = await _js.GetFromLocalStorage(Constants.ACCESS_TOKEN_EXPIRETIME_STORAGE_KEY);
        if (DateTime.TryParse(accessExpirationTime, out var expTime))
        {
            if (IsTokenExpired(expTime))
            { 
                token = await RefreshToken();
                if (string.IsNullOrEmpty(token))
                {
                    await CleanUp();
                    return Anonymous;
                }
            }
        }
        return BuildAuthenticationState(token);
    }

    private async Task<string> RefreshToken()
    {
        var refreshToken = await _js.GetFromLocalStorage(Constants.REFRESH_TOKEN_STORAGE_KEY);
        try
        {
            var response = await _client.GetAsync($"auth/refresh/{refreshToken}");
            if (response.IsSuccessStatusCode)
            {
                var tokens = await response.Content.ReadFromJsonAsync<TokenResponseVm>();
                await _js.SetInLocalStorage(Constants.ACCESS_TOKEN_STORAGE_KEY, tokens.AccessToken);
                await _js.SetInLocalStorage(Constants.ACCESS_TOKEN_EXPIRETIME_STORAGE_KEY, tokens.AccessTokenExpiresAt);
                await _js.SetInLocalStorage(Constants.REFRESH_TOKEN_STORAGE_KEY, tokens.RefreshToken);
                await _js.SetInLocalStorage(Constants.REFRESH_TOKEN_EXPIRETIME_STORAGE_KEY, tokens.RefreshTokenExpiresAt);
                return tokens.AccessToken;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
        return null;
    }

    private bool IsTokenExpired(DateTime expirationTime)
    {
        return expirationTime <= DateTime.UtcNow;
    }

    public AuthenticationState BuildAuthenticationState(string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }
    
    public async Task Login(TokenResponseVm model)
    {
        await _js.SetInLocalStorage(Constants.ACCESS_TOKEN_STORAGE_KEY, model.AccessToken);
        await _js.SetInLocalStorage(Constants.ACCESS_TOKEN_EXPIRETIME_STORAGE_KEY, model.AccessTokenExpiresAt);
        await _js.SetInLocalStorage(Constants.REFRESH_TOKEN_STORAGE_KEY, model.RefreshToken);
        await _js.SetInLocalStorage(Constants.REFRESH_TOKEN_EXPIRETIME_STORAGE_KEY, model.RefreshTokenExpiresAt);
        var authState = BuildAuthenticationState(model.AccessToken);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task Logout()
    {
        await CleanUp();
        NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
    }
    
    private async Task CleanUp()
    {
        await _js.RemoveFromLocalStorage(Constants.ACCESS_TOKEN_STORAGE_KEY);
        await _js.RemoveFromLocalStorage(Constants.ACCESS_TOKEN_EXPIRETIME_STORAGE_KEY);
        await _js.RemoveFromLocalStorage(Constants.REFRESH_TOKEN_STORAGE_KEY);
        await _js.RemoveFromLocalStorage(Constants.REFRESH_TOKEN_EXPIRETIME_STORAGE_KEY);
        _client.DefaultRequestHeaders.Authorization = null;
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles);
        keyValuePairs.TryGetValue(ClaimTypes.Email, out var email);
        claims.Add(new Claim(ClaimTypes.Email, email.ToString()));

        if (roles != null)
        {
            if (roles.ToString().Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                foreach (var parsedRole in parsedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
            }

            keyValuePairs.Remove(ClaimTypes.Role);
        }
        
        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

}