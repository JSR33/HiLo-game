using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLoGame.Contracts.v1
{
    public class ApiRoutes
    {
        public const string Version = "v1";
        public const string Root = "api";
        public const string Base = Root + "/" + Version;

        public static class GameInfo
        {
            public const string GetGameInfo = Base + "/gameinfo";
            public const string Update = Base + "/gameinfo/{id}";
        }
    }
}
