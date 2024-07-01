using Microsoft.Extensions.Configuration;
using Miratorg.TimeKeeper.BusinessLogic.Configs;
using Moq;
using Xunit;

namespace Miratorg.TimeKeeper.Tests.v4;

public class IBlockingConfigTests
{
    private Mock<IBlockingConfig> mockBlockingConfig = new Mock<IBlockingConfig>();

    public IBlockingConfigTests()
    {
        mockBlockingConfig.SetupGet(m => m.IsUse).Returns(true);
        mockBlockingConfig.SetupGet(m => m.Dates).Returns(new int[] { 1, 16 });
        mockBlockingConfig.SetupGet(m => m.Hour).Returns(8);
    }

    [Fact]
    public void CheckConfig()
    {
        // Arrange
        var blockingConfig = mockBlockingConfig.Object;

        // Act
        var result = blockingConfig.ChechAccess(DateTime.Now);

        // Assert
        Assert.True(blockingConfig.IsUse);

        // Act

        // Assert
        Assert.True(blockingConfig.IsUse);
        Assert.Equal(8, blockingConfig.Hour);
        Assert.Equal(1, blockingConfig.Dates[0]);
        Assert.Equal(16, blockingConfig.Dates[1]);
    }

    [Fact]
    public void CheckNotUse()
    {
        mockBlockingConfig.SetupGet(m => m.IsUse).Returns(false);

        var blockingConfig = mockBlockingConfig.Object;

        var result = blockingConfig.IsBlockedDay(new DateTime(2024, 1, 10), new DateTime(2024, 1, 10, 0, 0, 0));

        // Assert
        Assert.False(blockingConfig.IsUse);
        Assert.False(result);
    }

    [Fact]
    public void Check_next_date()
    {
        var blockingConfig = mockBlockingConfig.Object;

        DateTime currentDate = new DateTime(2024, 7, 1, 7, 0, 0);

        Assert.False(blockingConfig.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingConfig.IsBlockedDay(new DateTime(2025, 1, 1), currentDate));
    }

}
