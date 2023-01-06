using HiLoGame.Backend.Domain;

namespace HiLoGame.Backend.Services
{
    /// <summary>
    /// Business logic to play game
    /// </summary>
    public interface IPlayGameRepository
    {
        /// <summary>
        /// Atribution of magic numbers for all users
        /// </summary>
        /// <returns></returns>
        public Task<bool> CreateUsersMagicNumber();

        /// <summary>
        /// Validate if player already have a magic number associated
        /// </summary>
        /// <returns></returns>
        public int GetPlayerMagicNumber(int playerId);
        
        /// <summary>
        /// Validate if there are more rounds to be played
        /// </summary>
        /// <returns></returns>
        public Task<bool> HasMoreRoundsToPlay();
       
        /// <summary>
        /// Set next game round number
        /// </summary>
        /// <returns></returns>
        public Task<int> SetNextGameRoundNumber();
    }
}
