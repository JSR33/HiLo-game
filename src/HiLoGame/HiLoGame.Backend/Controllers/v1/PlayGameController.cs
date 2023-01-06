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
        /// Get next player to play
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(SetRoundReponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [HttpGet(ApiRoutes.PlayGame.CreateNewGameRoundNumber)]
        public async Task<IActionResult> CreateNewGameRoundNumber()
        {
            var isLastRound = await _playGameRepository.HasMoreRoundsToPlay();

            if (!isLastRound)
            {
                var roundNumber = await _playGameRepository.SetNextGameRoundNumber();

                if (!await _playGameRepository.CreateUsersMagicNumber())
                    return BadRequest(ErrorResponseObjectCreator.ErrorResponseObject($"It was not possible to create magic numbers for players."));

                return Ok(new Response<SetRoundReponse>(
                    new SetRoundReponse
                    {
                        NewRoundNumber = roundNumber,
                        EndOfGame = !isLastRound
                    }));
            }

            return Ok(new Response<SetRoundReponse>(
                new SetRoundReponse
                {
                    NewRoundNumber = 0,
                    EndOfGame = isLastRound
                }));
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
