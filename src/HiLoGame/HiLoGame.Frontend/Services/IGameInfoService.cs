using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;

namespace HiLoGame.Frontend.Services
{
    /// <summary>
    /// Game info frontend service
    /// </summary>
    public interface IGameInfoService
    {
        /// <summary>
        /// Get game modes from api
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetGameModes();

        /// <summary>
        /// Get game types from api
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetGameTypes();

        /// <summary>
        /// Get game info in memory from api 
        /// </summary>
        /// <returns></returns>
        Task<GameInfoResponse> GetGameInfo();

        /// <summary>
        /// Upgate game info in api memory 
        /// </summary>
        /// <param name="gameInfoRequest"></param>
        /// <returns></returns>
        Task<GameInfoResponse> UpdateGameInfo(GameInfoRequest gameInfoRequest);
    }
}
