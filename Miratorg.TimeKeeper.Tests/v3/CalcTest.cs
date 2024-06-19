using Miratorg.TimeKeeper.BusinessLogic;

namespace Miratorg.TimeKeeper.Tests.v3;

public class CalcTest
{
    [Fact]
    public void CalcTimeTest1()
    {
        DateTime begin = new DateTime(2023, 1, 1, 3, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 5, 0, 0);

        var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(begin, end);

        Assert.Equal(dayMinutes, 0);
        Assert.Equal(nightMinutes, 2 * 60);
    }

    [Fact]
    public void CalcTimeTest2()
    {
        DateTime begin = new DateTime(2023, 1, 1, 3, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 8, 0, 0);

        var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(begin, end);

        Assert.Equal(dayMinutes, 2 * 60);
        Assert.Equal(nightMinutes, 3 * 60);
    }

    [Fact]
    public void CalcTimeTest3()
    {
        DateTime begin = new DateTime(2023, 1, 1, 3, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 20, 0, 0);

        var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(begin, end);

        Assert.Equal(dayMinutes, 14 * 60);
        Assert.Equal(nightMinutes, 3 * 60);
    }

    [Fact]
    public void CalcTimeTest4()
    {
        DateTime begin = new DateTime(2023, 1, 1, 3, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 23, 0, 0);

        var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(begin, end);

        Assert.Equal(dayMinutes, 16 * 60);
        Assert.Equal(nightMinutes, 4 * 60);
    }

    [Fact]
    public void CalcTimeTest5()
    {
        DateTime begin = new DateTime(2023, 1, 1, 6, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 20, 0, 0);

        var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(begin, end);

        Assert.Equal(dayMinutes, 14 * 60);
        Assert.Equal(nightMinutes, 0 * 60);
    }

    [Fact]
    public void CalcTimeTest7()
    {
        DateTime begin = new DateTime(2023, 1, 1, 7, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 23, 0, 0);

        var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(begin, end);

        Assert.Equal(dayMinutes, 15 * 60);
        Assert.Equal(nightMinutes, 1 * 60);
    }

    [Fact]
    public void CalcTimeTest8()
    {
        DateTime begin = new DateTime(2023, 1, 1, 7, 0, 0);
        DateTime end = new DateTime(2023, 1, 1, 19, 0, 0);

        var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(begin, end);

        Assert.Equal(dayMinutes, 12 * 60);
        Assert.Equal(nightMinutes, 0 * 60);
    }
}