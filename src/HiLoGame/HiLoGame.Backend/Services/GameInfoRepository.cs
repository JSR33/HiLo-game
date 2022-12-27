using HiLoGame.Backend.Data;
using HiLoGame.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace HiLoGame.Backend.Services
{
    /// <inheritdoc/>
    public class GameInfoRepository : IGameInfoRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GameInfoRepository()
        {
            using (var context = new ApiContext())
            {
                if (context.GameInfo.Any(_ => _.Id == 1))
                    return;

                var gameInfo = new GameInfo
                {
                    Id = 1,
                    GameMode = GameMode.Easy,
                    GameType = GameType.SinglePlayer,
                    NumberOfRounds = 1
                };

                context.GameInfo.Add(gameInfo);
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public async Task<GameInfo> GetGameInfo(int id)
        {
            using (var context = new ApiContext())
            {
                return await context.GameInfo.FirstOrDefaultAsync(_ => _.Id == id);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateGameInfo(GameInfo gameInfo)
        {
            if(gameInfo == null) { return false; }

            using (var context = new ApiContext())
            {
                context.GameInfo.Update(gameInfo);
                int nrOfUpdated = await context.SaveChangesAsync();
                return nrOfUpdated > 0;
            }
        }
    }
}
