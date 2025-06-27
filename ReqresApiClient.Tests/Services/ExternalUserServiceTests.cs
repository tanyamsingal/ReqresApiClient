using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ReqresApiClient.Clients;
using ReqresApiClient.Models;
using ReqresApiClient.Services;
using Xunit;

namespace ReqresApiClient.Tests.Services;

public class ExternalUserServiceTests
{
    private readonly Mock<IReqresUserApiClient> _mockClient;
    private readonly ExternalUserService _service;

    public ExternalUserServiceTests()
    {
        _mockClient = new Mock<IReqresUserApiClient>();
        _service = new ExternalUserService(_mockClient.Object);
    }

    [Fact]
    public async Task GetUserByIdAsync_Returns_User()
    {
        var userId = 1;
        var expectedUser = new UserDto
        {
            Id = userId,
            First_Name = "John",
            Last_Name = "Doe",
            Email = "john.doe@example.com"
        };

        _mockClient
            .Setup(c => c.GetUserByIdAsync(userId))
            .ReturnsAsync(expectedUser);

        var result = await _service.GetUserByIdAsync(userId);

        Assert.Equal(expectedUser.Id, result.Id);
        Assert.Equal(expectedUser.Email, result.Email);
    }

    [Fact]
    public async Task GetAllUsersAsync_Returns_List_Of_Users()
    {
        var users = new List<UserDto>
        {
            new UserDto { Id = 1, First_Name = "Alice", Last_Name = "Smith", Email = "alice@example.com" },
            new UserDto { Id = 2, First_Name = "Bob", Last_Name = "Brown", Email = "bob@example.com" }
        };

        _mockClient
            .Setup(c => c.GetAllUsersAsync())
            .ReturnsAsync(users);

        var result = await _service.GetAllUsersAsync();

        Assert.NotNull(result);
        Assert.Equal(2, ((List<UserDto>)result).Count);
    }
}
