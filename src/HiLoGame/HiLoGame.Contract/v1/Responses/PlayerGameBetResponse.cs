namespace HiLoGame.Contracts.v1.Responses
{
    public class PlayerGameBetResponse
    {
        public int PlayerId { get; set; }
        public bool IsMagicalNumber { get; set; }
        public bool IsHigher { get; set; }
    }
}
