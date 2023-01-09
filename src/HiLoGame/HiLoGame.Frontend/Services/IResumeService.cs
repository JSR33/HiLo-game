using HiLoGame.Contracts.v1.Responses;

namespace HiLoGame.Frontend.Services
{
    public interface IResumeService
    {
        Task<IEnumerable<ResumePlayerGameInfo>> GetPlayersResume();
        Task<bool> EndGame();
        Task<bool> RestartGame();
    }
}
