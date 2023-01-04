using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HiLoGame.Backend.Domain
{
    /// <summary>
    /// Player information to be used during the game
    /// </summary>
    public class PlayerInfo
    {
        /// <summary>
        /// Player information key
        /// </summary>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Player name
        /// </summary>
        [NotNull]
        [StringLength(150, MinimumLength = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Player age
        /// </summary>
        [Range(1,99)]
        public int Age { get; set; }

        /// <summary>
        /// Number of rounds played
        /// </summary>
        public int GameRound { get; set; }
    }
}
