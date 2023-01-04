using AutoMapper;
using HiLoGame.Backend.Services;
using HiLoGame.Contracts.v1;
using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HiLoGame.Backend.Controllers.v1
{
    /// <summary>
    /// Play game controller actions
    /// </summary>
    [Produces("application/json")]
    public class PlayGameController : Controller
    {
        private IPlayGameRepository _playGameRepository;
        private IMapper _mapper;

        public PlayGameController(IPlayGameRepository playGameRepository, IMapper mapper)
        {
            _playGameRepository = playGameRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create users magic numbers action
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [HttpGet(ApiRoutes.PlayGame.CreateUsersMagicNumber)]
        public async Task<IActionResult> CreateUsersMagicNumber()
        {
            if (! await _playGameRepository.CreateUsersMagicNumber())
                return BadRequest(ErrorResponseObjectCreator.ErrorResponseObject($"It was not possible to create magic numbers for players."));

            return Ok();
        }

        /// <summary>
        /// Get next player to play
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(SetRoundReponse), 200)]
        [HttpGet(ApiRoutes.PlayGame.GetNextPlayerInformation)]
        public async Task<IActionResult> SetNextGameRoundNumber()
        {
            var hasMoreRounds = await _playGameRepository.HasMoreRoundsToPlay();

            if (!hasMoreRounds)
            {
                var roundNumber = await _playGameRepository.SetNextGameRoundNumber();
                return Ok(new Response<SetRoundReponse>(
                    new SetRoundReponse
                    {
                        NewRoundNumber = roundNumber,
                        EndOfGame = !hasMoreRounds
                    }));
            }

            return Ok(new Response<SetRoundReponse>(
                new SetRoundReponse
                {
                    NewRoundNumber = 0,
                    EndOfGame = !hasMoreRounds
                }));
        }

        /// <summary>
        /// Get next player to play
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(PlayerInfoResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [HttpGet(ApiRoutes.PlayGame.GetNextPlayerInformation)]
        public async Task<IActionResult> GetNextPlayerInformation(int roundNumber)
        {
            var nextPlayer = await _playGameRepository.GetNextPlayerInfo(roundNumber);
            if(nextPlayer.Id == 0)
                return NotFound(ErrorResponseObjectCreator.ErrorResponseObject($"Next player not found"));

            return Ok(new Response<PlayerInfoResponse>(_mapper.Map<PlayerInfoResponse>(nextPlayer)));
        }

        /// <summary>
        /// Validate user bet 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PlayerGameBetResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [HttpPut(ApiRoutes.PlayGame.PlayerBet)]
        public async Task<IActionResult> PlayerBet([FromBody] PlayerGameBetRequest playerGameBetRequest)
        {
            var playerMagicNumber = _playGameRepository.GetPlayerMagicNumber(playerGameBetRequest.PlayerId);

            if (playerMagicNumber == 0)
            {
                return BadRequest(ErrorResponseObjectCreator.ErrorResponseObject($"Player with Id {playerGameBetRequest.PlayerId} doesn't have a magical number."));
            }

            PlayerGameBetResponse response = new PlayerGameBetResponse
            {
                PlayerId = playerGameBetRequest.PlayerId,
                IsMagicalNumber = playerGameBetRequest.MagicNumberBet == playerMagicNumber,
                IsHigher = playerGameBetRequest.MagicNumberBet < playerMagicNumber
            };

            return Ok(new Response<PlayerGameBetResponse>(response));
        }
    }
}
