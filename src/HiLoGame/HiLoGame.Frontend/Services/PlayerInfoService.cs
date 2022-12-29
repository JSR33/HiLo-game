using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;
using System.Net.Http.Json;
using System.Text.Json;

namespace HiLoGame.Frontend.Services
{
    public class PlayerInfoService : IPlayerInfoService
    {
        private const string RequestUriCreatePlayerInfo = "api/v1/createnewplayer";
        private readonly HttpClient _httpClient;

        public PlayerInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PlayerInfoResponse> CreateNewPlayer(PlayerInfoRequest playerInfo)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync(RequestUriCreatePlayerInfo, playerInfo);
                httpResponse.EnsureSuccessStatusCode();
                return await HttpResponseToGameInfoResponse(httpResponse);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        /// <summary>
        /// Convert from <see cref="HttpResponseMessage"/> to <see cref="PlayerInfoResponse"/>
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        private async Task<PlayerInfoResponse> HttpResponseToGameInfoResponse(HttpResponseMessage httpResponse)
        {
            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<PlayerInfoResponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (responseData is null)
            {
                return new PlayerInfoResponse();
            }

            return responseData.Data;
        }
    }
}
