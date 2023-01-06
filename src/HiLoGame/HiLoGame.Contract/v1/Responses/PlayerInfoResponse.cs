namespace HiLoGame.Contracts.v1.Responses
{
    public class PlayerInfoResponse
    {
        /// <summary>
        /// Player information key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Player name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Player age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Number of rounds played
        /// </summary>
        public int GameRound { get; set; }
    }
}
