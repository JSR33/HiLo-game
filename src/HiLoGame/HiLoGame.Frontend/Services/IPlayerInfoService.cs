using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;

namespace HiLoGame.Frontend.Services
{
    /// <summary>
    /// Player info frontend service
    /// </summary>
    public interface IPlayerInfoService
    {
        Task<PlayerInfoResponse> CreateNewPlayer(PlayerInfoRequest playerInfo);
    }
}
