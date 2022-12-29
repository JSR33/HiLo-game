
using HiLoGame.Backend.Data;
using HiLoGame.Backend.Domain;

namespace HiLoGame.Backend.Services
{
    /// <inheritdoc/>
    public class PlayerInfoRepository : IPlayerInfoRepository
    {
        /// <inheritdoc/>
        public async Task<bool> CreateNewPlayer(PlayerInfo player)
        {
            if (player == null)
                return false;

            using (var context = new ApiContext())
            {
                int lastId = 0;

                if(context.PlayerInfo.Any())
                    lastId = context.PlayerInfo.OrderBy(_ => _.Id).Last().Id;

                player.Id = ++lastId;

                await context.PlayerInfo.AddAsync(player);
                var created = await context.SaveChangesAsync();
                return created > 0;
            }
        }
    }
}
