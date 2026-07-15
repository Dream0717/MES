using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MES.Desktop.Services;

public class ApiResponse<T>
{
    [JsonPropertyName("success")] public bool Success { get; set; }
    [JsonPropertyName("data")] public T? Data { get; set; }
    [JsonPropertyName("message")] public string? Message { get; set; }
}

public class LoginRequest
{
    [JsonPropertyName("username")] public string Username { get; set; } = "";
    [JsonPropertyName("password")] public string Password { get; set; } = "";
}

public class LoginResult
{
    [JsonPropertyName("token")] public string Token { get; set; } = "";
    [JsonPropertyName("username")] public string Username { get; set; } = "";
    [JsonPropertyName("realName")] public string RealName { get; set; } = "";
    [JsonPropertyName("roleName")] public string RoleName { get; set; } = "";
}

public class DashboardStats
{
    [JsonPropertyName("activeOrders")] public int ActiveOrders { get; set; }
    [JsonPropertyName("pendingOrders")] public int PendingOrders { get; set; }
    [JsonPropertyName("todayCompleted")] public int TodayCompleted { get; set; }
    [JsonPropertyName("totalDefects")] public int TotalDefects { get; set; }
}

public class MesApiService
{
    readonly HttpClient _http;
    public MesApiService(HttpClient h) => _http = h;

    public string? Token
    {
        get => _http.DefaultRequestHeaders.Authorization?.Parameter;
        set => _http.DefaultRequestHeaders.Authorization = value is null ? null : new("Bearer", value);
    }

    public async Task<ApiResponse<LoginResult>> LoginAsync(LoginRequest r)
    {
        var res = await _http.PostAsJsonAsync("api/auth/login", r);
        return (await res.Content.ReadFromJsonAsync<ApiResponse<LoginResult>>()) ?? new() { Success = false, Message = "请求失败" };
    }

    public async Task<ApiResponse<List<object>>> GetProductionProgressAsync()
    {
        try { return await _http.GetFromJsonAsync<ApiResponse<List<object>>>("api/dashboard/production-progress") ?? new(); }
        catch { return new() { Success = false, Message = "连接失败" }; }
    }
}
