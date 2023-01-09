using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLoGame.Contracts.v1.Responses
{
    public class MaxBetValueResponse
    {
        /// <summary>
        /// Maximum number that player can bet for configured level
        /// </summary>
        public int MaxValue { get; set; }
    }
}
