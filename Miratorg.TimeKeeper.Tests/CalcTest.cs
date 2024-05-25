using Miratorg.TimeKeeper.BusinessLogic;

namespace Miratorg.TimeKeeper.Tests;

public class CalcTest
{
    [Fact]
    public void CalcTimeTest1()
    {
        DateTime begin = new DateTime(2023, 1, 1, 3, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 5, 0, 0);

        var (dayHours, nightHours) = TimeKeeperConverter.CalculateDayAndNightHours(begin, end);

        Assert.Equal(dayHours, 0);
        Assert.Equal(nightHours, 2 * 60);
    }

    [Fact]
    public void CalcTimeTest2()
    {
        DateTime begin = new DateTime(2023, 1, 1, 3, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 8, 0, 0);

        var (dayHours, nightHours) = TimeKeeperConverter.CalculateDayAndNightHours(begin, end);

        Assert.Equal(dayHours, 2 * 60);
        Assert.Equal(nightHours, 3 * 60);
    }

    [Fact]
    public void CalcTimeTest3()
    {
        DateTime begin = new DateTime(2023, 1, 1, 3, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 20, 0, 0);

        var (dayHours, nightHours) = TimeKeeperConverter.CalculateDayAndNightHours(begin, end);

        Assert.Equal(dayHours, 14 * 60);
        Assert.Equal(nightHours, 3 * 60);
    }

    [Fact]
    public void CalcTimeTest4()
    {
        DateTime begin = new DateTime(2023, 1, 1, 3, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 23, 0, 0);

        var (dayHours, nightHours) = TimeKeeperConverter.CalculateDayAndNightHours(begin, end);

        Assert.Equal(dayHours, 16 * 60);
        Assert.Equal(nightHours, 4 * 60);
    }

    [Fact]
    public void CalcTimeTest5()
    {
        DateTime begin = new DateTime(2023, 1, 1, 6, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 20, 0, 0);

        var (dayHours, nightHours) = TimeKeeperConverter.CalculateDayAndNightHours(begin, end);

        Assert.Equal(dayHours, 14 * 60);
        Assert.Equal(nightHours, 0 * 60);
    }

    [Fact]
    public void CalcTimeTest7()
    {
        DateTime begin = new DateTime(2023, 1, 1, 7, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 23, 0, 0);

        var (dayHours, nightHours) = TimeKeeperConverter.CalculateDayAndNightHours(begin, end);

        Assert.Equal(dayHours, 15 * 60);
        Assert.Equal(nightHours, 1 * 60);
    }

    [Fact]
    public void CalcTimeTest8()
    {
        DateTime begin = new DateTime(2023, 1, 1, 7, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 19, 0, 0);

        var (dayHours, nightHours) = TimeKeeperConverter.CalculateDayAndNightHours(begin, end);

        Assert.Equal(dayHours, 12 * 60);
        Assert.Equal(nightHours, 0 * 60);
    }
}