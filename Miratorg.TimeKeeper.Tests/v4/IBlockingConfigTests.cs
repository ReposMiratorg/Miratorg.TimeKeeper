using Microsoft.Extensions.Configuration;
using Miratorg.TimeKeeper.BusinessLogic.Configs;
using Moq;

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
}
