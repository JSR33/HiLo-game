using HiLoGame.Backend.Data;
using HiLoGame.Backend.Domain;
using HiLoGame.Contracts.v1.Responses;
using Microsoft.EntityFrameworkCore;

namespace HiLoGame.Backend.Services
{
    /// <inheritdoc/>
    public class ResumeRepository : IResumeRepository
    {
        public async Task<bool> EndGame()
        {
            using(var context = new ApiContext())
            {
                context.PlayerGameInfo.RemoveRange(context.PlayerGameInfo.ToList());
                context.PlayerInfo.RemoveRange(context.PlayerInfo.ToList());
                context.GameInfo.First().RoundNumber = 0;
                context.SaveChanges();
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ResumePlayerGameInfo>> GetResumePlayersGameInfo()
        {
            List<ResumePlayerGameInfo> resumePlayersGameInfo = new List<ResumePlayerGameInfo>();

            using(var context = new ApiContext())
            {
                foreach (var playerGameInfo in context.PlayerGameInfo)
                {
                    var player = await context.PlayerInfo.FirstOrDefaultAsync(_ => _.Id == playerGameInfo.PlayerInfoId);

                    resumePlayersGameInfo.Add(new ResumePlayerGameInfo
                    {
                        Pontuation = playerGameInfo.Pontuation,
                        PlayerName = player.Name
                    });
                }
            }

            return resumePlayersGameInfo;
        }

        /// <inheritdoc/>
        public async Task<bool> RestartGame()
        {
            using (var context = new ApiContext())
            {
                context.PlayerGameInfo.RemoveRange(context.PlayerGameInfo.ToList());
                context.GameInfo.First().RoundNumber = 0;

                context.SaveChanges();
            }

            return true;
        }
    }
}
