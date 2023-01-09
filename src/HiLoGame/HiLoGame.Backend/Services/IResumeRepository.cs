using HiLoGame.Contracts.v1.Responses;

namespace HiLoGame.Backend.Services
{
    /// <summary>
    /// Business logic to resume
    /// </summary>
    public interface IResumeRepository
    {
        /// <summary>
        /// Get players pontuation
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ResumePlayerGameInfo>> GetResumePlayersGameInfo();

        /// <summary>
        /// Do all cleanups to end game
        /// </summary>
        /// <returns></returns>
        Task<bool> EndGame();

        /// <summary>
        /// Do all cleanups to restart game
        /// </summary>
        /// <returns></returns>
        Task<bool> RestartGame();
    }
}
