using HiLoGame.Contracts.v1.Responses;
using HiLoGame.Contracts.v1;
using System.Net;
using System.Text.Json;
using Xunit;
using FluentAssertions;
using HiLoGame.Backend.Domain;
using HiLoGame.Contracts.v1.Requests;
using System.Net.Http.Json;
using System.ComponentModel;
using HiLoGame.Backend.Data;

namespace HiLoGame.Tests.IntegrationTests
{
    public class PlayGameTests : IntegrationTestsBase
    {
        [Fact]
        public async Task CreateNewGameRound_ReturnOk()
        {
            int numberOfRounds = 4;
            var fakeGameInfoRequest = new GameInfoRequest
            {
                GameMode = GameMode.Easy,
                GameType = GameType.SinglePlayer,
                NumberOfRounds = numberOfRounds
            };

            var httpResponseGameInfo = await TestClient.PutAsJsonAsync(ApiRoutes.GameInfo.Update.Replace("{id}", "1"), fakeGameInfoRequest);
            httpResponseGameInfo.EnsureSuccessStatusCode();

            for (int i = 1; i <= numberOfRounds + 1; i++)
            {
                var httpResponse = await TestClient.GetAsync(ApiRoutes.PlayGame.CreateNewGameRoundNumber);
                var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<Response<SetRoundReponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                httpResponse.Should().NotBeNull();
                httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                responseData.Should().NotBeNull();
                
                if(i <= numberOfRounds)
                {
                    responseData.Data.Should().BeEquivalentTo(new SetRoundReponse
                    {
                        NewRoundNumber = i,
                        EndOfGame = false
                    });
                }
                else
                {
                    responseData.Data.Should().BeEquivalentTo(new SetRoundReponse
                    {
                        NewRoundNumber = 0,
                        EndOfGame = true
                    });
                }
            }
        }

        [Fact]
        public async Task PlayerBet_ReturnOk()
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

            var betRequest = new PlayerGameBetRequest
            {
                MagicNumberBet = 15,
                PlayerId = 1
            };

            var httpResponse = await TestClient.PutAsJsonAsync(ApiRoutes.PlayGame.PlayerBet, betRequest);
            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<PlayerGameBetResponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            responseData.Should().NotBeNull();
        }


        [Theory]
        [InlineData("Easy", HttpStatusCode.OK, 25)]
        [InlineData("Medium", HttpStatusCode.OK, 100)]
        [InlineData("Hard", HttpStatusCode.OK, 500)]
        [InlineData("NOK", HttpStatusCode.OK, 0)]
        public async Task GetMaxBetValue_ReturnOk(string gameMode, HttpStatusCode expectedResult, int expectedBetValue)
        {
            var httpResponse = await TestClient.GetAsync(ApiRoutes.PlayGame.GetMaxBetValue.Replace("{gameMode}", gameMode));
            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<MaxBetValueResponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(expectedResult);
            responseData.Should().NotBeNull();
            responseData.Data.MaxValue.Should().Be(expectedBetValue);
        }
    }
}
