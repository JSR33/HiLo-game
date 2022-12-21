using HiLoGame.Backend.Domain;
using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace HiLoGame.Backend.SwaggerExamples.Responses
{
    /// <summary>
    /// Game info response swagger example
    /// </summary>
    public class GameInfoResponseExample : IExamplesProvider<GameInfoResponse>
    {
        /// <inheritdoc/>
        public GameInfoResponse GetExamples()
        {
            return new GameInfoResponse
            {
                Id = 1,
                GameMode = GameMode.Easy,
                GameType = GameType.SinglePlayer,
                NumberOfRounds = 2
            };
        }
    }
}
