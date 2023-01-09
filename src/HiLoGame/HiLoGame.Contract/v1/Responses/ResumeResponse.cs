namespace HiLoGame.Contracts.v1.Responses
{
    public class ResumeResponse
    {
        /// <summary>
        /// Game pontuations of all players
        /// </summary>
        public IEnumerable<ResumePlayerGameInfo> ResumePlayersGameInfo { get; set; }
    }
}
