﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLoGame.Contracts.v1.Responses
{
    public class SetRoundReponse
    {
        public int NewRoundNumber { get; set; }
        public bool EndOfGame { get; set; }
    }
}
