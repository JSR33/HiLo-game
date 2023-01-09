namespace HiLoGame.Contracts.v1.Requests
{
    public class PlayerGameBetRequest
    {
        /// <summary>
        /// Playerd ID
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Player magic number bet
        /// </summary>
        public int MagicNumberBet { get; set; }
    }
}
