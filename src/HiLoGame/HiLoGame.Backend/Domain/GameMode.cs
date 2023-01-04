namespace HiLoGame.Backend.Domain
{
    /// <summary>
    /// Enumerator of all game modes
    /// </summary>
    public class GameMode
    {
        /// <summary>
        /// Easy mode
        /// </summary>
        public const string Easy = "Easy";

        /// <summary>
        /// Medium mode
        /// </summary>
        public const string Medium = "Medium";

        /// <summary>
        /// Hard mode
        /// </summary>
        public const string Hard = "Hard";

        /// <summary>
        /// Returns a value that indicates if the <paramref name="gameMode"/> is one of the availables in the game
        /// </summary>
        /// <param name="gameMode"></param>
        /// <returns></returns>
        public static bool IsValid(string gameMode)
        {
            return IsEasyMode(gameMode)
                || IsMediumMode(gameMode)
                || IsHardMode(gameMode);
        }

        public static int GetModeHigherMagicNumber(string gameMode)
        {
            switch (gameMode)
            {
                case Easy:
                    return 25;
                case Medium:
                    return 100;
                case Hard:
                    return 500;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Returns a value that indicates if the <paramref name="gameMode"/> is <see cref="Easy"/>
        /// </summary>
        /// <param name="gameMode"></param>
        /// <returns></returns>
        private static bool IsEasyMode(string gameMode)
        {
            return gameMode.Equals(Easy);
        }

        /// <summary>
        /// Returns a value that indicates if the <paramref name="gameMode"/> is <see cref="Medium"/>
        /// </summary>
        /// <param name="gameMode"></param>
        /// <returns></returns>
        private static bool IsMediumMode(string gameMode)
        {
            return gameMode.Equals(Medium);
        }

        /// <summary>
        /// Returns a value that indicates if the <paramref name="gameMode"/> is <see cref="Hard"/>
        /// </summary>
        /// <param name="gameMode"></param>
        /// <returns></returns>
        private static bool IsHardMode(string gameMode)
        {
            return gameMode.Equals(Hard);
        }
    }
}
