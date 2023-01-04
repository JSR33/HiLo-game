using HiLoGame.Backend.Data;
using HiLoGame.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace HiLoGame.Backend.Services
{
    /// <inheritdoc/>
    public class PlayGameRepository : IPlayGameRepository
    {
        /// <inheritdoc/>
        public Task<bool> CreateUsersMagicNumber()
        {
            using(var context = new ApiContext())
            {
                var gameMode = context.GameInfo.First().GameMode;
                var MagicNumberMaxNumber = GameMode.GetModeHigherMagicNumber(gameMode);

                if (MagicNumberMaxNumber == 0)
                    return Task.FromResult(false);

                foreach (var userInfo in context.PlayerInfo)
                {
                    var playerGameInfo = new PlayerGameInfo()
                    {
                        PlayerInfoId = userInfo.Id,
                        Pontuation = 0,
                        MagicNumber = new Random().Next(0, MagicNumberMaxNumber)
                    };

                    context.PlayerGameInfo.Add(playerGameInfo);
                    context.SaveChanges();
                }
            }

            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public async Task<PlayerInfo> GetNextPlayerInfo(int roundNumber)
        {
            using (var context = new ApiContext())
            {
                if (!await context.PlayerInfo.AnyAsync())
                    return new PlayerInfo();

                return SetPlayerRoundAndReturnIt(context, roundNumber, await context.PlayerInfo.Where(_ => _.GameRound < roundNumber).OrderBy(_ => _.Id).FirstAsync());
            }
        }

        /// <inheritdoc/>
        public int GetPlayerMagicNumber(int playerId)
        {
            using (var context = new ApiContext())
            {
                return context.PlayerGameInfo.First(_ => _.PlayerInfoId == playerId).MagicNumber;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> HasMoreRoundsToPlay()
        {
            using (var context = new ApiContext())
            {
                var gameInfo = await context.GameInfo.FirstAsync();
                return gameInfo.NumberOfRounds == gameInfo.RoundNumber;
            }
        }

        /// <inheritdoc/>
        public async Task<int> SetNextGameRoundNumber()
        {
            using (var context = new ApiContext())
            {
                var gameInfo = await context.GameInfo.FirstAsync();
                gameInfo.RoundNumber++;

                context.GameInfo.Update(gameInfo);
                context.SaveChanges();

                return gameInfo.RoundNumber;
            }
        }

        /// <summary>
        /// Update player round number and return ir updated
        /// </summary>
        /// <param name="context"></param>
        /// <param name="gameRoundNumber"></param>
        /// <param name="playerInfo"></param>
        /// <returns></returns>
        private PlayerInfo SetPlayerRoundAndReturnIt(ApiContext context, int gameRoundNumber, PlayerInfo? playerInfo)
        {
            if (playerInfo == null)
                return new PlayerInfo();

            playerInfo.GameRound = gameRoundNumber;

            context.PlayerInfo.Update(playerInfo);
            context.SaveChanges();

            return playerInfo;
        }
    }
}
