using System.ComponentModel.DataAnnotations;

namespace HiLoGame.Backend.Domain
{
    /// <summary>
    /// Player game information to be used during the game
    /// </summary>
    public class PlayerGameInfo
    {
        /// <inheritdoc/>
        [Key]
        public int PlayerInfoId { get; set; }

        /// <summary>
        /// User pontuation
        /// </summary>
        public int Pontuation { get; set; }

        /// <summary>
        /// Magic number to be guessed by user
        /// </summary>
        public int MagicNumber { get; set; }
    }
}
