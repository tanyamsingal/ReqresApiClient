# Reqres API Client (.NET)

This project demonstrates consuming a public API using .NET 8 with HttpClient, async/await, and pagination handling.

## Projects

- `ReqresApiClient` – Class Library with core logic
- `ReqresApiClient.Tests` – Unit tests (xUnit + Moq)
- `ReqresApiClient.ConsoleDemo` – Simple console app demo

## How to Run

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project ReqresApiClient.ConsoleDemo
