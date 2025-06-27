using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReqresApiClient.Clients;
using ReqresApiClient.Services;
using System.Net.Http; 
using System;        

var builder = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        string baseUrl = "https://reqres.in/api";
        services.AddHttpClient<ReqresUserApiClient>();
        services.AddSingleton(sp =>
        {
            var httpClient = sp.GetRequiredService<HttpClient>();
            return new ReqresUserApiClient(httpClient, baseUrl);
        });
        services.AddSingleton<ExternalUserService>();
    });

var app = builder.Build();

var service = app.Services.GetRequiredService<ExternalUserService>();

Console.WriteLine("Fetching all users...\n");

try
{
    var users = await service.GetAllUsersAsync();

    foreach (var user in users)
    {
        Console.WriteLine($"{user.Id}: {user.First_Name} {user.Last_Name} - {user.Email}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error fetching users: {ex.Message}");
}
