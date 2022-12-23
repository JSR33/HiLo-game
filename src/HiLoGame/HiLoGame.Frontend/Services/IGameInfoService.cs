using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;

namespace HiLoGame.Frontend.Services
{
    public interface IGameInfoService
    {
        Task<IEnumerable<string>> GetGameModes();
        Task<IEnumerable<string>> GetGameTypes();
        Task<GameInfoResponse> GetGameInfo();
        Task<GameInfoResponse> UpdateGameInfo(GameInfoRequest gameInfoRequest);
    }
}
