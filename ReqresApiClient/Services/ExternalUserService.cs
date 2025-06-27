using ReqresApiClient.Models;
using ReqresApiClient.Clients;

namespace ReqresApiClient.Services;

public class ExternalUserService
{
    private readonly IReqresUserApiClient _client;

    public ExternalUserService(IReqresUserApiClient client)
    {
        _client = client;
    }

    public Task<UserDto> GetUserByIdAsync(int id)
    {
        return _client.GetUserByIdAsync(id);
    }

    public Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return _client.GetAllUsersAsync();
    }
}
