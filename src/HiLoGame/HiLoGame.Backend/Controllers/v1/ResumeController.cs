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
    }
}
