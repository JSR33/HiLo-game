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
    }
}
