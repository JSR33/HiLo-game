using AutoMapper;
using HiLoGame.Backend.Domain;
using HiLoGame.Contracts.v1.Responses;

namespace HiLoGame.Backend.Mapping
{
    /// <summary>
    /// Domain to response model mapper
    /// </summary>
    public class DomainToResponseProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DomainToResponseProfile()
        {
            CreateMap<GameInfo, GameInfoResponse>();
        }
    }
}
