using HiLoGame.Backend.Data;
using HiLoGame.Backend.Domain;
using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;
using Microsoft.EntityFrameworkCore;

namespace HiLoGame.Backend.Services
{
    /// <inheritdoc/>
    public class PlayGameRepository : IPlayGameRepository
    {
        /// <inheritdoc/>
        public async Task<bool> CreateUsersMagicNumber()
        {
            using (var context = new ApiContext())
            {
                var gameMode = context.GameInfo.First().GameMode;
                var MagicNumberMaxNumber = GameMode.GetModeHigherMagicNumber(gameMode);

                if (MagicNumberMaxNumber == 0)
                    return false;

                foreach (var userInfo in context.PlayerInfo)
                {
                    PlayerGameInfo playerGameInfo;

                    if (!await context.PlayerGameInfo.AnyAsync(_ => _.PlayerInfoId == userInfo.Id))
                    {
                        playerGameInfo = new PlayerGameInfo()
                        {
                            PlayerInfoId = userInfo.Id,
                            Pontuation = 0,
                            MagicNumber = new Random().Next(0, MagicNumberMaxNumber)
                        };

                        context.PlayerGameInfo.Add(playerGameInfo);
                    }
                    else
                    {
                        playerGameInfo = await context.PlayerGameInfo.FirstAsync(_ => _.PlayerInfoId == userInfo.Id);
                        playerGameInfo.MagicNumber = new Random().Next(0, MagicNumberMaxNumber);

                        context.PlayerGameInfo.Update(playerGameInfo);
                    }

                    context.SaveChanges();
                }
            }

            return true;
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

        /// <inheritdoc/>
        public async Task<PlayerGameBetResponse> ValidatePlayerBet(int playerId, int betNumber, int playerMagicNumber)
        {
            var isBetCorrect = betNumber == playerMagicNumber;
            
            bool isMagicNumberHigherThenBet = false;
            if (!isBetCorrect)
            {
                isMagicNumberHigherThenBet = betNumber < playerMagicNumber;

                using(var context = new ApiContext())
                {
                    var player = await context.PlayerGameInfo.FirstOrDefaultAsync(_ => _.PlayerInfoId == playerId);

                    if (player != null)
                    {
                        player.Pontuation += 1;
                        context.PlayerGameInfo.Update(player);
                        context.SaveChanges();
                    }
                }
            }

            return new PlayerGameBetResponse
            {
                PlayerId = playerId,
                IsMagicalNumber = isBetCorrect,
                IsHigher = isMagicNumberHigherThenBet
            };
        }
    }
}
