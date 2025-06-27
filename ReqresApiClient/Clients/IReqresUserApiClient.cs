using ReqresApiClient.Models;

namespace ReqresApiClient.Clients;

public interface IReqresUserApiClient
{
    Task<UserDto> GetUserByIdAsync(int userId);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
}
