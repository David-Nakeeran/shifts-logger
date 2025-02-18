

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
}