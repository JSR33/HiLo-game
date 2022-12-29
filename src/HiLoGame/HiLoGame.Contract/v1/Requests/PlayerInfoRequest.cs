using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HiLoGame.Contracts.v1.Requests
{
    public class PlayerInfoRequest
    {
        /// <summary>
        /// Player name
        /// </summary>
        [NotNull]
        [StringLength(150, MinimumLength = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Player age
        /// </summary>
        [Range(1, 99)]
        public int Age { get; set; }
    }
}
