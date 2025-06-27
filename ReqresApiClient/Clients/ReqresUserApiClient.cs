using System.Text.Json;
using ReqresApiClient.Models;

namespace ReqresApiClient.Clients;

public class ReqresUserApiClient : IReqresUserApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ReqresUserApiClient(HttpClient httpClient, string baseUrl)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl.TrimEnd('/');
    }

    public async Task<UserDto> GetUserByIdAsync(int userId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/users/{userId}");

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"User with ID {userId} not found. Status code: {response.StatusCode}");

        var content = await response.Content.ReadAsStringAsync();
        var wrapper = JsonSerializer.Deserialize<UserWrapper>(content);

        return wrapper?.Data ?? throw new InvalidOperationException("User data missing in response.");
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var allUsers = new List<UserDto>();
        int page = 1;
        UsersListResponse responsePage;

        do
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/users?page={page}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            responsePage = JsonSerializer.Deserialize<UsersListResponse>(content);

            if (responsePage?.Data != null)
                allUsers.AddRange(responsePage.Data);

            page++;
        }
        while (responsePage != null && page <= responsePage.Total_Pages);

        return allUsers;
    }

    private class UserWrapper
    {
        public UserDto Data { get; set; }
    }
}
