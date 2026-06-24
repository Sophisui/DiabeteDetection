using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.JSInterop;

namespace FrontendService.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    public string? Token { get; private set; }
    public bool IsAuthenticated => Token != null;

    public AuthService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login", new { username, password });
        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        Token = result.GetProperty("token").GetString();

        if (Token != null)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "jwt_token", Token);
            SetAuthHeader();
        }
        return Token != null;
    }

    public async Task InitializeAsync()
    {
        if (Token == null)
            Token = await _js.InvokeAsync<string?>("localStorage.getItem", "jwt_token");
        SetAuthHeader();
    }

    public void SetAuthHeader()
    {
        if (Token != null)
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Token);
    }
}