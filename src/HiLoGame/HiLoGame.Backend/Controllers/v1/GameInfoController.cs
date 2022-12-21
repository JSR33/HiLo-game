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
    /// Game info controller actions
    /// </summary>
    [Produces("application/json")]
    public class GameInfoController : Controller
    {
        private readonly IGameInfoRepository _gameInfoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="gameInfoRepository"></param>
        /// <param name="mapper"></param>
        public GameInfoController(IGameInfoRepository gameInfoRepository, IMapper mapper)
        {
            _gameInfoRepository = gameInfoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Return game info configured in system
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(GameInfoResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [HttpGet(ApiRoutes.GameInfo.GetGameInfo)]
        public async Task<IActionResult> GetGameInfo()
        {
            var gameInfo = await _gameInfoRepository.GetGameInfo(1);

            if (gameInfo == null)
            {
                return NotFound(ErrorResponseObjectCreator.ErrorResponseObject($"There are no game configurations in the system"));
            }

            return Ok(new Response<GameInfoResponse>(_mapper.Map<GameInfoResponse>(gameInfo)));
        }

        /// <summary>
        /// Return game info configured in system
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.GameInfo.GetGameTypes)]
        public List<string> GetGameTypes()
        {
            var gameTypes = new List<string>() 
            { 
                GameType.SinglePlayer, 
                GameType.MultiPlayer 
            };

            return gameTypes;
        }

        /// <summary>
        /// Return game info configured in system
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.GameInfo.GetGameModes)]
        public List<string> GetGameModes()
        {
            var gameMode = new List<string>() 
            { 
                GameMode.Easy, 
                GameMode.Medium, 
                GameMode.Hard
            };

            return gameMode;
        }

        /// <summary>
        /// Update game information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GameInfoResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [HttpPut(ApiRoutes.GameInfo.Update)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GameInfoRequest request)
        {
            if (!GameType.IsValid(request.GameType))
            {
                return BadRequest(ErrorResponseObjectCreator.ErrorResponseObject($"Game configuration with id = {id} not updated. Game type not valid."));
            }

            if (!GameMode.IsValid(request.GameMode))
            {
                return BadRequest(ErrorResponseObjectCreator.ErrorResponseObject($"Game configuration with id = {id} not updated. Game mode not valid."));
            }

            var gameConfig = await _gameInfoRepository.GetGameInfo(id);
            if(gameConfig == null)
            {
                return NotFound(ErrorResponseObjectCreator.ErrorResponseObject($"Game configuration with id = {id} not found in the system."));
            }

            gameConfig.GameType = request.GameType;
            gameConfig.GameMode = request.GameMode;
            gameConfig.NumberOfRounds = request.NumberOfRounds;

            var updated = await _gameInfoRepository.UpdateGameInfo(gameConfig);

            if (!updated)
            {
                return BadRequest(ErrorResponseObjectCreator.ErrorResponseObject($"Game configuration with id = {id} not updated."));
            }

            return Ok(new Response<GameInfoResponse>(_mapper.Map<GameInfoResponse>(gameConfig)));
        }
    }
}
