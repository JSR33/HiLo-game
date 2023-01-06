using HiLoGame.Contracts.v1.Responses;

namespace HiLoGame.Frontend.Services
{
    public interface IPlayGameService
    {
        Task<SetRoundReponse> CreateNewGameRoundNumber();

        Task<PlayerGameBetResponse> PlayerBet(int playerId, int playerBet);

        Task<int> GetMaxBetValue(string gameMode);
    }
}
