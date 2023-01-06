using AutoMapper;
using HiLoGame.Backend.Domain;
using HiLoGame.Backend.Services;
using HiLoGame.Contracts.v1;
using HiLoGame.Contracts.v1.Requests;
using HiLoGame.Contracts.v1.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        /// <param name="playerGameBetRequest"></param>
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

            PlayerGameBetResponse response = await _playGameRepository.ValidatePlayerBet(playerGameBetRequest.PlayerId, playerGameBetRequest.MagicNumberBet, playerMagicNumber);

            return Ok(new Response<PlayerGameBetResponse>(response));
        }

        [ProducesResponseType(typeof(MaxBetValueResponse), 200)]
        [HttpGet(ApiRoutes.PlayGame.GetMaxBetValue)]
        public async Task<IActionResult> GetMaxBetValue([FromRoute] string gameMode)
        {
            return Ok(new Response<MaxBetValueResponse>(
                new MaxBetValueResponse
                {
                    MaxValue = GameMode.GetModeHigherMagicNumber(gameMode)
                }));
        }
    }
}
