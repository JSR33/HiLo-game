using FluentAssertions;
using HiLoGame.Backend.Domain;
using HiLoGame.Backend.Services;
using Xunit;

namespace HiLoGame.Tests.UnitTests
{
    public class PlayerInfoRepositoryTests
    {
        [Fact]
        public async Task CreatePlayer_ReturnTrue()
        {
            var playerInfo = new PlayerInfo
            {
                Name = "John",
                Age = 29
            };

            var service = new PlayerInfoRepository();
            bool result = await service.CreateNewPlayer(playerInfo);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CreatePlayer_WithNullModel_ReturnFalse()
        {
            var service = new PlayerInfoRepository();
            bool result = await service.CreateNewPlayer(null);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateMultiPlayers_With_ReturnTrue()
        {
            var playerOneInfo = new PlayerInfo
            {
                Name = "John",
                Age = 29
            };

            var playerTwoInfo = new PlayerInfo
            {
                Name = "Peter",
                Age = 55
            };

            var service = new PlayerInfoRepository();
            bool resultPlayerOne = await service.CreateNewPlayer(playerOneInfo);
            bool resultPlayerTwo = await service.CreateNewPlayer(playerTwoInfo);

            resultPlayerOne.Should().BeTrue();
            resultPlayerTwo.Should().BeTrue();
        }
    }
}
