using FluentAssertions;
using HiLoGame.Backend.Domain;
using HiLoGame.Backend.Services;
using Xunit;

namespace HiLoGame.Tests.UnitTests
{
    public class GameInfoRepositoryTests
    {
        [Fact]
        public async Task GetGameInfo_ReturnOk()
        {
            var expectedGameInfo = new GameInfo
            {
                Id = 1,
                GameMode = GameMode.Easy,
                GameType = GameType.SinglePlayer,
                NumberOfRounds = 1
            };

            var gameInfoRepository = new GameInfoRepository();
            var result = await gameInfoRepository.GetGameInfo(1);
            
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedGameInfo);
        }

        [Fact]
        public async Task GetGameInfo_NotValidId_ReturnNull()
        {
            var expectedGameInfo = new GameInfo
            {
                Id = 1,
                GameMode = GameMode.Easy,
                GameType = GameType.SinglePlayer,
                NumberOfRounds = 1
            };

            var gameInfoRepository = new GameInfoRepository();
            var result = await gameInfoRepository.GetGameInfo(2);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateGameInfo_ReturnTrue()
        {
            var gameInfo = new GameInfo
            {
                Id = 1,
                GameMode = GameMode.Easy,
                GameType = GameType.SinglePlayer,
                NumberOfRounds = 1
            };

            var gameInfoRepository = new GameInfoRepository();
            var result = await gameInfoRepository.UpdateGameInfo(gameInfo);

            result.Should().BeTrue();
        }
    }
}
