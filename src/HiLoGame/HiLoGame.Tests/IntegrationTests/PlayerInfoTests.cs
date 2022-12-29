using FluentAssertions;
using HiLoGame.Contracts.v1;
using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace HiLoGame.Tests.IntegrationTests
{
    public class PlayerInfoTests : IntegrationTestsBase
    {
        public static IEnumerable<object[]> InvalidPlayersModelValues =>
        new List<object[]>
        {
            new object[] { new PlayerInfoRequest { Name = "", Age = 29} },
            new object[] { new PlayerInfoRequest { Age = 29} },
            new object[] { new PlayerInfoRequest { Name = "John", Age = 0 } },
            new object[] { new PlayerInfoRequest { Name = "John", Age = 100 } }
        };

        [Fact]
        public async Task CreateNewPlayer_ValidateResult()
        {
            var newPlayerInfoRequest = new PlayerInfoRequest
            {
                Name = "John Smith",
                Age = 29
            };

            var newPlayerInfoResponse = new PlayerInfoResponse
            {
                Id = 1,
                Name = "John Smith",
                Age = 29
            };

            var httpResponse = await TestClient.PostAsJsonAsync(ApiRoutes.PlayerInfo.CreateNewPlayer, newPlayerInfoRequest);

            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<PlayerInfoResponse>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            responseData.Should().NotBeNull();
            responseData.Data.Should().BeEquivalentTo(newPlayerInfoResponse);
        }

        [Theory]
        [MemberData(nameof(InvalidPlayersModelValues))]
        public async Task CreateNewPlayer_WithInvalidModelValues_ReturnBadRequest(PlayerInfoRequest playerInfoRequest)
        {
            
            var httpResponse = await TestClient.PostAsJsonAsync(ApiRoutes.PlayerInfo.CreateNewPlayer, playerInfoRequest);
            var response = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseData = JsonSerializer.Deserialize<ErrorResponse>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            responseData.Should().NotBeNull();
            responseData.Errors.First().Message.Should().NotBeNull();
        }
    }
}
