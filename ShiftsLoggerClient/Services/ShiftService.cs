using System.Net.Http.Headers;
using System.Text.Json;
using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient.Services;

class ShiftService
{
    private readonly HttpClient _httpClient;

    public ShiftService()
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

    public async Task<List<ShiftDTO>> GetAllShifts()
    {
        try
        {
            var requestUri = "shifts";

            await using Stream stream = await _httpClient.GetStreamAsync(requestUri);

            var shifts = await JsonSerializer.DeserializeAsync<List<ShiftDTO>>(stream);

            return shifts ?? new List<ShiftDTO>();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex.Message}");
            return new List<ShiftDTO>();
        }
    }

    public async Task<ShiftDTO> GetShiftById(long id)
    {
        try
        {
            var requestUri = $"shifts/{id}";
            await using Stream stream = await _httpClient.GetStreamAsync(requestUri);

            var shift = await JsonSerializer.DeserializeAsync<ShiftDTO>(stream);
            return shift ?? new ShiftDTO { };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex.Message}");
            return new ShiftDTO { };
        }
    }

    internal async Task<ApiResponse<bool>> DeleteShiftById(long id)
    {
        try
        {
            var requestUri = $"shifts/{id}";
            using HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Shift has been deleted successfully",
                    StatusCode = response.StatusCode
                };
            }

            return new ApiResponse<bool>
            {
                Success = false,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex.Message}");
            return null;
        }
    }
}