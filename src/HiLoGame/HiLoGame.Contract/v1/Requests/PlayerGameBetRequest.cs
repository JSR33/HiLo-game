using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLoGame.Contracts.v1.Requests
{
    public class PlayerGameBetRequest
    {
        public int PlayerId { get; set; }
        public int MagicNumberBet { get; set; }
    }
}
