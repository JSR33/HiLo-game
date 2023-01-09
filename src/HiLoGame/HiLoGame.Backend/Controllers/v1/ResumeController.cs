using AutoMapper;
using HiLoGame.Backend.Services;
using HiLoGame.Contracts.v1;
using HiLoGame.Contracts.v1.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HiLoGame.Backend.Controllers.v1
{
    /// <summary>
    /// Resume controller actions
    /// </summary>
    [Produces("application/json")]
    public class ResumeController : Controller
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;

        public ResumeController(IResumeRepository resumeRepository, IMapper mapper)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get players pontuation
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResumeResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [HttpGet(ApiRoutes.Resume.ResumeTable)]
        public async Task<IActionResult> GetGameInfo()
        {
            var resumePlayersGameInfo = await _resumeRepository.GetResumePlayersGameInfo();

            if (!resumePlayersGameInfo.Any())
            {
                return NotFound(ErrorResponseObjectCreator.ErrorResponseObject($"There are no players pontuations to show."));
            }

            var response = new ResumeResponse
            {
                ResumePlayersGameInfo = resumePlayersGameInfo
            };

            return Ok(new Response<ResumeResponse>(response));
        }

        /// <summary>
        /// Do all cleanups to end game
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet(ApiRoutes.Resume.EndGame)]
        public async Task<IActionResult> EndGame()
        {
            if (!await _resumeRepository.EndGame())
                return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Do all cleanups to restart game
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet(ApiRoutes.Resume.RestartGame)]
        public async Task<IActionResult> RestartGame()
        {
            if (!await _resumeRepository.RestartGame())
                return BadRequest();

            return Ok();
        }
    }
}
