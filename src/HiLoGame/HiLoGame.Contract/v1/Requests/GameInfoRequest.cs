using System.ComponentModel.DataAnnotations;

namespace HiLoGame.Contracts.v1.Requests
{
    public  class GameInfoRequest
    {
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
        [Range(1,6)]
        public int NumberOfRounds { get; set; }
    }
}
