using HiLoGame.Backend.Domain;

namespace HiLoGame.Backend.Services
{
    /// <summary>
    /// Business logic to update game info
    /// </summary>
    public interface IGameInfoRepository
    {
        /// <summary>
        /// Get game info by <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<GameInfo> GetGameInfo(int id);

        /// <summary>
        /// Update game info
        /// </summary>
        /// <returns></returns>
        public Task<bool> UpdateGameInfo(GameInfo gameInfo);
    }
}
