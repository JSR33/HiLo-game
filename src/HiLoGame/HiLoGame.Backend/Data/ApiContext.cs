using Microsoft.EntityFrameworkCore;
using HiLoGame.Backend.Domain;

namespace HiLoGame.Backend.Data
{
    /// <summary>
    /// In memory database context
    /// </summary>
    public class ApiContext : DbContext
    {
        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "HiLoGameDb");
        }

        internal DbSet<GameInfo> GameInfo { get; set; }
        internal DbSet<PlayerInfo> PlayerInfo { get; set; }
    }
}
