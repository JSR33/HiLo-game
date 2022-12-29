using AutoMapper;
using HiLoGame.Backend.Domain;
using HiLoGame.Backend.Services;
using HiLoGame.Contracts.v1;
using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HiLoGame.Backend.Controllers.v1
{
    /// <summary>
    /// Player information controller actions
    /// </summary>
    [Produces("application/json")]
    public class PlayerInfoController : Controller
    {
        private readonly IPlayerInfoRepository _playerInfoRepository;
        private readonly IMapper _mapper;

        public PlayerInfoController(IPlayerInfoRepository playerInfoRepository, IMapper mapper)
        {
            _playerInfoRepository = playerInfoRepository;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(PlayerInfoResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [HttpPost(ApiRoutes.PlayerInfo.CreateNewPlayer)]
        public async Task<IActionResult> CreateNewUser([FromBody] PlayerInfoRequest playerInfoRequest)
        {
            var newPlayerInfo = new PlayerInfo
            {
                Name = playerInfoRequest.Name,
                Age = playerInfoRequest.Age
            };

            bool playerCreated = await _playerInfoRepository.CreateNewPlayer(newPlayerInfo);

            if (!playerCreated)
            {
                return BadRequest(ErrorResponseObjectCreator.ErrorResponseObject($"Player configuration not created."));
            }

            return Ok(new Response<PlayerInfoResponse>(_mapper.Map<PlayerInfoResponse>(newPlayerInfo)));
        }
        
    }
}
