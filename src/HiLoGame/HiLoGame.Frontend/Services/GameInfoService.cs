using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;
using System.Net.Http.Json;
using System.Text.Json;

namespace HiLoGame.Frontend.Services
{
    public class GameInfoService : IGameInfoService
    {
        private const string RequestUriGameModes = "api/v1/gamemodes";
        private const string RequestUriGameTypes = "api/v1/gametypes";
        private const string RequestUriGameInfo = "api/v1/gameinfo";
        private const string RequestUriGameInfoUpdate = "api/v1/gameinfo/1";
        private readonly HttpClient _httpClient;

        public GameInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GameInfoResponse> GetGameInfo()
        {
            using var httpResponse =  await _httpClient.GetAsync(RequestUriGameInfo);
            httpResponse.EnsureSuccessStatusCode();
            return await HttpResponseToGameInfoResponse(httpResponse);
        }

        public async Task<IEnumerable<string>> GetGameModes()
        {
            IEnumerable<string>? gameModes = await _httpClient.GetFromJsonAsync<IEnumerable<string>>(RequestUriGameModes);

            if (gameModes is null)
                gameModes = Array.Empty<string>();

            return gameModes;
        }

        public async Task<IEnumerable<string>> GetGameTypes()
        {
            IEnumerable<string>? gameTypes = await _httpClient.GetFromJsonAsync<IEnumerable<string>>(RequestUriGameTypes);

            if (gameTypes is null)
                gameTypes = Array.Empty<string>();

            return gameTypes;
        }

        public async Task<GameInfoResponse> UpdateGameInfo(GameInfoRequest gameInfoRequest)
        {
            using var httpResponse = await _httpClient.PutAsJsonAsync(RequestUriGameInfoUpdate, gameInfoRequest);
            httpResponse.EnsureSuccessStatusCode();
            return await HttpResponseToGameInfoResponse(httpResponse);
        }

        private async Task<GameInfoResponse> HttpResponseToGameInfoResponse(HttpResponseMessage httpResponse)
        {
            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<GameInfoResponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (responseData is null)
            {
                return new GameInfoResponse();
            }

            return responseData.Data;
        }
    }
}
