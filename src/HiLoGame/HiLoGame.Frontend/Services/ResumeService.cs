using HiLoGame.Contracts.v1.Responses;
using System.Text.Json;

namespace HiLoGame.Frontend.Services
{
    public class ResumeService : IResumeService
    {
        private const string RequestUriResume = "api/v1/resumetable";
        private const string RequestUriEndGame = "api/v1/endgame";
        private const string RequestUriRestartGame= "api/v1/restartgame";
        private readonly HttpClient _httpClient;

        public ResumeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> EndGame()
        {
            var httpResponse = await _httpClient.GetAsync(RequestUriEndGame);
            httpResponse.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<IEnumerable<ResumePlayerGameInfo>> GetPlayersResume()
        {
            using var httpResponse = await _httpClient.GetAsync(RequestUriResume);
            httpResponse.EnsureSuccessStatusCode();
            var resumeResponse = await HttpResponseToResumeResponse(httpResponse);
            return resumeResponse.ResumePlayersGameInfo;
        }

        public async Task<bool> RestartGame()
        {
            var httpResponse = await _httpClient.GetAsync(RequestUriRestartGame);
            httpResponse.EnsureSuccessStatusCode();
            return true;
        }

        /// <summary>
        /// Convert from <see cref="HttpResponseMessage"/> to <see cref="GameInfoResponse"/>
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        private async Task<ResumeResponse> HttpResponseToResumeResponse(HttpResponseMessage httpResponse)
        {
            var contentPlainText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Response<ResumeResponse>>(contentPlainText, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (responseData is null)
            {
                return new ResumeResponse();
            }

            return responseData.Data;
        }
    }
}
