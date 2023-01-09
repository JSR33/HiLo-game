namespace HiLoGame.Contracts.v1.Responses
{
    public class PlayerGameBetResponse
    {
        /// <summary>
        /// Player ID number
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Validation if the player bet is or not the magical number
        /// </summary>
        public bool IsMagicalNumber { get; set; }

        /// <summary>
        /// Inform if player bet number is higher or not than maginal number
        /// </summary>
        public bool IsHigher { get; set; }
    }
}
