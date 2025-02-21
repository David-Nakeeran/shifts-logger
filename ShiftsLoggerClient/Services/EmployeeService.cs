

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient.Services;

class EmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7029/api/")
        };

        _httpClient.DefaultRequestHeaders.Accept.Clear();

        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ShiftsLoggerClient");
    }

    internal async Task<ApiResponse<List<EmployeeDTO>>> GetAllEmployees()
    {
        try
        {
            var requestUri = "employee";

            using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return new ApiResponse<List<EmployeeDTO>>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
            var employees = await response.Content.ReadFromJsonAsync<List<EmployeeDTO>>()
                ?? new List<EmployeeDTO>();

            return new ApiResponse<List<EmployeeDTO>>
            {
                Success = true,
                Message = "Success",
                Data = employees
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex.Message}");
            return new ApiResponse<List<EmployeeDTO>>
            {
                Success = false,
                Message = $"Error message: {ex.Message}"
            };
        }
    }
}