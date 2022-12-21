using System.ComponentModel.DataAnnotations;

namespace HiLoGame.Backend.Domain
{
    /// <summary>
    /// Game information to be used to create the game
    /// </summary>
    public class GameInfo
    {
        /// <summary>
        /// Model key
        /// </summary>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Game type (Single or Multi  player)
        /// </summary>
        public string GameType { get; set; }

        /// <summary>
        /// Game mode (Easy, Medium, Hard)
        /// </summary>
        public string GameMode { get; set; }

        /// <summary>
        /// Number of rounds to be played to complete a game
        /// </summary>
        public int NumberOfRounds { get; set; }
    }
}
