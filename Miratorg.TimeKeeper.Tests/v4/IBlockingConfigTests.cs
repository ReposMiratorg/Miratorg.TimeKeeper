using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Miratorg.TimeKeeper.BusinessLogic.Configs;
using Moq;
using Xunit;

namespace Miratorg.TimeKeeper.Tests.v4;

public class IBlockingConfigTests
{
    private readonly IBlockingService blockingService;
    private readonly Mock<IOptions<BlockingConfig>> mockOptionsBlockConfig;
    private BlockingConfig config = new BlockingConfig();

    public IBlockingConfigTests()
    {
        config.Hour = 8;
        config.IsUse = true;

        mockOptionsBlockConfig = new Mock<IOptions<BlockingConfig>>();
        mockOptionsBlockConfig.Setup(x => x.Value).Returns(config);

        blockingService = new BlockingService(mockOptionsBlockConfig.Object);
    }

    [Fact]
    public void CheckNotUse()
    {
        config.IsUse = false;

        var result = blockingService.IsBlockedDay(new DateTime(2024, 1, 10), new DateTime(2024, 1, 10, 0, 0, 0));

        Assert.False(result);
    }

    [Fact]
    public void Check_next_date()
    {
        DateTime currentDate = new DateTime(2024, 7, 1, 7, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2025, 1, 1), currentDate));
    }

    [Fact]
    public void Check_old_date()
    {
        DateTime currentDate = new DateTime(2024, 7, 1, 7, 0, 0);

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
    }

    [Fact]
    public void Check_blocked_date_1()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 7, 1, 7, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_2()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 7, 1, 8, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_3()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 7, 1, 9, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_4()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 7, 16, 7, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_5()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 7, 16, 8, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_6()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 7, 16, 9, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_7()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 7, 31, 7, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_8()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 7, 31, 8, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_9()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 7, 31, 9, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_10()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 8, 1, 7, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_11()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 8, 1, 8, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_12()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 8, 1, 9, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_8day()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 8, 8, 9, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }

    [Fact]
    public void Check_blocked_date_20day()
    {
        // Если дата календаря относится к прошлым месяцам, тогда блокируем после 8 часов первого дня (с 16 до 31) - с 1 до 15 заблокировано в любом случае
        DateTime currentDate = new DateTime(2024, 8, 20, 9, 0, 0);

        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 5), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 9, 1), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 30), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 23), currentDate));
        Assert.False(blockingService.IsBlockedDay(new DateTime(2024, 8, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 8, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 8, 6), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 8, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 10), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 7, 1), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 30), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 20), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 16), currentDate));

        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 8), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 6, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 31), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 16), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 15), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 14), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 2), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 5, 1), currentDate));
        Assert.True(blockingService.IsBlockedDay(new DateTime(2024, 4, 30), currentDate));
    }
}
