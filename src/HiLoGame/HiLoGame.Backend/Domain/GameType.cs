namespace HiLoGame.Backend.Domain
{
    /// <summary>
    /// Enumerator of all types of possible games
    /// </summary>
    public class GameType
    {
        /// <summary>
        /// Single player game type
        /// </summary>
        public static readonly string SinglePlayer = "Single Player";

        /// <summary>
        /// Multi player game type
        /// </summary>
        public static readonly string MultiPlayer = "Multi Player";

        /// <summary>
        /// Returns a value that indicates if the <paramref name="gameType"/> is one of the availables in the game
        /// </summary>
        /// <param name="gameType"></param>
        /// <returns></returns>
        public static bool IsValid(string gameType)
        {
            return IsSinglePlayer(gameType)
                || IsMultiplayer(gameType);
        }

        /// <summary>
        /// Returns a value that indicates if the <paramref name="gameType"/> is <see cref="SinglePlayer"/>
        /// </summary>
        /// <param name="gameType"></param>
        /// <returns></returns>
        private static bool IsMultiplayer(string gameType)
        {
            return gameType.Equals(MultiPlayer);
        }

        /// <summary>
        /// Returns a value that indicates if the <paramref name="gameType"/> is <see cref="MultiPlayer"/>
        /// </summary>
        /// <param name="gameType"></param>
        /// <returns></returns>
        private static bool IsSinglePlayer(string gameType)
        {
            return gameType.Equals(SinglePlayer);
        }
    }
}
