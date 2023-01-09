using HiLoGame.Backend.Domain;
using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1;
using System.Net.Http.Json;
using Xunit;
using HiLoGame.Contracts.v1.Responses;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace HiLoGame.Tests.IntegrationTests
{
    public class ResumeTests : IntegrationTestsBase
    {
        public ResumeTests()
        {
            var fakeGameInfoRequest = new GameInfoRequest
            {
                GameMode = GameMode.Easy,
                GameType = GameType.SinglePlayer,
                NumberOfRounds = 1
            };

            var httpResponseGameInfo = TestClient.PutAsJsonAsync(ApiRoutes.GameInfo.Update.Replace("{id}", "1"), fakeGameInfoRequest);
            httpResponseGameInfo.Result.EnsureSuccessStatusCode();

            var newPlayerInfoRequest = new PlayerInfoRequest
            {
                Name = "John",
                Age = 29
            };

            var httpResponseNewPlayer = TestClient.PostAsJsonAsync(ApiRoutes.PlayerInfo.CreateNewPlayer, newPlayerInfoRequest);
            httpResponseNewPlayer.Result.EnsureSuccessStatusCode();

            var httpResponseCreateRound = TestClient.GetAsync(ApiRoutes.PlayGame.CreateNewGameRoundNumber);
            httpResponseCreateRound.Result.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetGameInfo_ReturnOk()
        {
            //CreateNeededInfoToTestController();

            var httpResponseResume = await TestClient.GetAsync(ApiRoutes.Resume.ResumeTable);
            var contentPlainText = await httpResponseResume.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<ResumeResponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            httpResponseResume.Should().NotBeNull();
            httpResponseResume.StatusCode.Should().Be(HttpStatusCode.OK);
            httpResponseResume.Should().NotBeNull();
            responseData.Data.ResumePlayersGameInfo.Should().HaveCount(1);
        }

        [Fact]
        public async Task EndGame_ReturnOk()
        {
            //CreateNeededInfoToTestController();

            var httpResponseResume = await TestClient.GetAsync(ApiRoutes.Resume.EndGame);

            httpResponseResume.Should().NotBeNull();
            httpResponseResume.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task RestartGame_ReturnOk()
        {
            //CreateNeededInfoToTestController();

            var httpResponseResume = await TestClient.GetAsync(ApiRoutes.Resume.RestartGame);

            httpResponseResume.Should().NotBeNull();
            httpResponseResume.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private async void CreateNeededInfoToTestController()
        {
            var fakeGameInfoRequest = new GameInfoRequest
            {
                GameMode = GameMode.Easy,
                GameType = GameType.SinglePlayer,
                NumberOfRounds = 1
            };

            var httpResponseGameInfo = await TestClient.PutAsJsonAsync(ApiRoutes.GameInfo.Update.Replace("{id}", "1"), fakeGameInfoRequest);
            httpResponseGameInfo.EnsureSuccessStatusCode();

            var newPlayerInfoRequest = new PlayerInfoRequest
            {
                Name = "John",
                Age = 29
            };

            var httpResponseNewPlayer = await TestClient.PostAsJsonAsync(ApiRoutes.PlayerInfo.CreateNewPlayer, newPlayerInfoRequest);
            httpResponseNewPlayer.EnsureSuccessStatusCode();

            var httpResponseCreateRound = await TestClient.GetAsync(ApiRoutes.PlayGame.CreateNewGameRoundNumber);
            httpResponseCreateRound.EnsureSuccessStatusCode();

        }
    }
}
