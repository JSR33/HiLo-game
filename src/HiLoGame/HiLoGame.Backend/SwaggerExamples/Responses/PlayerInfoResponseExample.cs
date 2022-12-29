using HiLoGame.Contracts.v1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace HiLoGame.Backend.SwaggerExamples.Responses
{
    /// <summary>
    /// Player info response swagger example
    /// </summary>
    public class PlayerInfoResponseExample : IExamplesProvider<PlayerInfoResponse>
    {
        public PlayerInfoResponse GetExamples()
        {
            return new PlayerInfoResponse
            {
                Id = 1,
                Name = "John Smith",
                Age = 29
            };
        }
    }
}
