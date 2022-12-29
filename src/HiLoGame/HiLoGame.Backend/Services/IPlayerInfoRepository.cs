using HiLoGame.Backend.Domain;

namespace HiLoGame.Backend.Services
{
    /// <summary>
    /// Player info business logic
    /// </summary>
    public interface IPlayerInfoRepository
    {
        /// <summary>
        /// Create new player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Task<bool> CreateNewPlayer(PlayerInfo player);
    }
}
