using HiLoGame.Contracts.v1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace HiLoGame.Backend.SwaggerExamples.Requests
{
    /// <summary>
    /// Player info request swagger example
    /// </summary>
    public class PlayerInfoRequestExample : IExamplesProvider<PlayerInfoRequest>
    {
        public PlayerInfoRequest GetExamples()
        {
            return new PlayerInfoRequest
            {
                Name = "John Smith",
                Age = 29
            };
        }
    }
}
