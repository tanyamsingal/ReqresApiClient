namespace ReqresApiClient.Models;

public class UsersListResponse
{
    public int Page { get; set; }
    public int Per_Page { get; set; }
    public int Total { get; set; }
    public int Total_Pages { get; set; }
    public List<UserDto> Data { get; set; }
}
