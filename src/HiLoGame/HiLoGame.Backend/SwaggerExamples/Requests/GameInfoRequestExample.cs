using HiLoGame.Backend.Domain;
using HiLoGame.Contracts.v1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace HiLoGame.Backend.SwaggerExamples.Requests
{
    /// <summary>
    /// Game info request swagger example
    /// </summary>
    public class GameInfoRequestExample : IExamplesProvider<GameInfoRequest>
    {
        /// <inheritdoc/>
        public GameInfoRequest GetExamples()
        {
            return new GameInfoRequest
            {
                GameMode = GameMode.Easy,
                GameType = GameType.SinglePlayer,
                NumberOfRounds = 2
            };
        }
    }
}
