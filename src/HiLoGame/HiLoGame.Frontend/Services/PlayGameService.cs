using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;
using System.Net.Http.Json;
using System.Text.Json;

namespace HiLoGame.Frontend.Services
{
    public class PlayGameService : IPlayGameService
    {
        private const string RequestUriCreateNewRound = "api/v1/createnewgameroundnumber";
        private const string RequestUriPlayerBet = "api/v1/playerbet";
        private readonly HttpClient _httpClient;

        public PlayGameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<SetRoundReponse> CreateNewGameRoundNumber()
        {
            var httpResponse = await _httpClient.GetAsync(RequestUriCreateNewRound);
            httpResponse.EnsureSuccessStatusCode();

            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<SetRoundReponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (responseData is null)
            {
                return new SetRoundReponse();
            }

            return responseData.Data;
        }

        public async Task<PlayerGameBetResponse> PlayerBet(int playerId, int playerBet)
        {
            var playerBetRequest = new PlayerGameBetRequest
            {
                PlayerId = playerId,
                MagicNumberBet = playerBet
            };

            var httpResponse = await _httpClient.PutAsJsonAsync(RequestUriPlayerBet, playerBetRequest);
            httpResponse.EnsureSuccessStatusCode();

            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<PlayerGameBetResponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (responseData is null)
            {
                return new PlayerGameBetResponse();
            }

            return responseData.Data;
        }
    }
}
