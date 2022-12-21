using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int NumberOfRounds { get; set; }
    }
}
