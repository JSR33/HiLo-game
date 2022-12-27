using HiLoGame.Contracts.v1.Responses;
using HiLoGame.Contracts.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using FluentAssertions;
using HiLoGame.Backend.Domain;
using HiLoGame.Contracts.v1.Requests;

namespace HiLoGame.Tests.IntegrationTests
{
    public class GameInfoTests : IntegrationTestsBase
    {
        [Fact]
        public async Task GetGameInfo_ReturnOk()
        {
            var httpResponse = await TestClient.GetAsync(ApiRoutes.GameInfo.GetGameInfo);
            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<GameInfoResponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            responseData.Should().NotBeNull();

            GameMode.IsValid(responseData.Data.GameMode).Should().BeTrue();
            GameType.IsValid(responseData.Data.GameType).Should().BeTrue();
            responseData.Data.NumberOfRounds.Should().BeInRange(1,6);
        }

        [Fact]
        public async Task UpdateGameInfo_ValidRequest_ReturnOk()
        {
            var fakeGameInfoRequest = new GameInfoRequest
            {
                GameMode = GameMode.Hard,
                GameType = GameType.MultiPlayer,
                NumberOfRounds = 5
            };

            var fakeGameInfoResponse = new GameInfoResponse
            {
                Id = 1,
                GameMode = GameMode.Hard,
                GameType = GameType.MultiPlayer,
                NumberOfRounds = 5
            };

            var httpResponse = await TestClient.PutAsJsonAsync(ApiRoutes.GameInfo.Update.Replace("{id}", "1"), fakeGameInfoRequest);
            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<GameInfoResponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            responseData.Should().NotBeNull();
            responseData.Data.Should().BeEquivalentTo(fakeGameInfoResponse);
        }

        [Fact]
        public async Task UpdateGameInfo_NotFoundRequest_ReturnOk()
        {
            var fakeGameInfoRequest = new GameInfoRequest
            {
                GameMode = GameMode.Hard,
                GameType = GameType.MultiPlayer,
                NumberOfRounds = 5
            };

            var httpResponse = await TestClient.PutAsJsonAsync(ApiRoutes.GameInfo.Update.Replace("{id}", "2"), fakeGameInfoRequest);
            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<GameInfoResponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateGameInfo_GameModeBadRequest_ReturnOk()
        {
            var expectedErrorMessage = "Game configuration with id = 1 not updated. Game mode not valid.";

            var fakeGameInfoRequest = new GameInfoRequest
            {
                GameMode = "SomeBadMode",
                GameType = GameType.MultiPlayer,
                NumberOfRounds = 5
            };

            var httpResponse = await TestClient.PutAsJsonAsync(ApiRoutes.GameInfo.Update.Replace("{id}", "1"), fakeGameInfoRequest);
            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseData = JsonSerializer.Deserialize<ErrorResponse>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            responseData.Should().NotBeNull();
            responseData.Errors.First().Message.Should().Be(expectedErrorMessage);
        }

        [Fact]
        public async Task UpdateGameInfo_GameTypeBadRequest_ReturnOk()
        {
            var expectedErrorMessage = "Game configuration with id = 1 not updated. Game type not valid.";
            var fakeGameInfoRequest = new GameInfoRequest
            {
                GameMode = GameMode.Hard,
                GameType = "SomeBadType",
                NumberOfRounds = 5
            };

            var httpResponse = await TestClient.PutAsJsonAsync(ApiRoutes.GameInfo.Update.Replace("{id}", "1"), fakeGameInfoRequest);
            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseData = JsonSerializer.Deserialize<ErrorResponse>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            
            responseData.Should().NotBeNull();
            responseData.Errors.First().Message.Should().Be(expectedErrorMessage);
        }

        [Fact]
        public void GetGameTypes_ReturnOk()
        {
            var fakeGameTypes = new List<string>()
            {
                GameType.SinglePlayer,
                GameType.MultiPlayer
            };

            var responseData = TestClient.GetFromJsonAsync<IEnumerable<string>>(ApiRoutes.GameInfo.GetGameTypes);

            responseData.Should().NotBeNull();
            responseData.Result.Should().BeEquivalentTo(fakeGameTypes); 
        }

        [Fact]
        public void GetGameModes_ReturnOk()
        {
            var fakeGameMode = new List<string>()
            {
                GameMode.Easy,
                GameMode.Medium,
                GameMode.Hard
            };

            var responseData = TestClient.GetFromJsonAsync<IEnumerable<string>>(ApiRoutes.GameInfo.GetGameModes);

            responseData.Should().NotBeNull();
            responseData.Result.Should().BeEquivalentTo(fakeGameMode);
        }
    }
}
