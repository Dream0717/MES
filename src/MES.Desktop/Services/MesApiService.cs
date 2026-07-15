using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MES.Desktop.Services;

// ── API DTOs ──────────────────────────────────────────

public class ApiResponse<T>
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

public class LoginRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("realName")]
    public string RealName { get; set; } = string.Empty;

    [JsonPropertyName("roleName")]
    public string RoleName { get; set; } = string.Empty;
}

public class ProductionProgress
{
    [JsonPropertyName("workOrderId")]
    public int WorkOrderId { get; set; }

    [JsonPropertyName("orderNo")]
    public string OrderNo { get; set; } = string.Empty;

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("targetQuantity")]
    public int TargetQuantity { get; set; }

    [JsonPropertyName("completedQuantity")]
    public int CompletedQuantity { get; set; }

    [JsonPropertyName("defectQuantity")]
    public int DefectQuantity { get; set; }

    [JsonPropertyName("completionRate")]
    public double CompletionRate { get; set; }

    [JsonPropertyName("qualityRate")]
    public double QualityRate { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }
}

// ── API Service ──────────────────────────────────────

public class MesApiService
{
    private readonly HttpClient _http;

    public MesApiService(HttpClient http)
    {
        _http = http;
    }

    public string? Token
    {
        get => _http.DefaultRequestHeaders.Authorization?.Parameter;
        set
        {
            _http.DefaultRequestHeaders.Authorization =
                string.IsNullOrEmpty(value) ? null :
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", value);
        }
    }

    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request);
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();
        return result ?? new ApiResponse<LoginResponse> { Success = false, Message = "请求失败" };
    }

    public async Task<ApiResponse<List<ProductionProgress>>> GetProductionProgressAsync()
    {
        var result = await _http.GetFromJsonAsync<ApiResponse<List<ProductionProgress>>>("api/dashboard/production-progress");
        return result ?? new ApiResponse<List<ProductionProgress>> { Success = false };
    }

    public async Task<ApiResponse<object>> GetDashboardStatsAsync()
    {
        var result = await _http.GetFromJsonAsync<ApiResponse<object>>("api/dashboard/stats");
        return result ?? new ApiResponse<object> { Success = false };
    }

    public string GetApiBaseUrl() => _http.BaseAddress?.ToString() ?? "http://localhost:5000/";
}
