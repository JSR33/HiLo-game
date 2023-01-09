using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLoGame.Contracts.v1.Responses
{
    public class SetRoundReponse
    {
        /// <summary>
        /// New round number created
        /// </summary>
        public int NewRoundNumber { get; set; }

        /// <summary>
        /// Information about if the game has ended
        /// </summary>
        public bool EndOfGame { get; set; }
    }
}
