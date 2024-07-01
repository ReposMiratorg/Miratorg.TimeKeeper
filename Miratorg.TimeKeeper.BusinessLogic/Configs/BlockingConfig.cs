using Microsoft.Extensions.Options;

namespace Miratorg.TimeKeeper.BusinessLogic.Configs;

public class BlockingConfig
{
    public bool IsUse { get; set; }
    public int Hour { get; set; }
}

public interface IBlockingService
{
    bool ChechAccess(DateTime date);
    bool IsBlockedDay(DateTime calendarDay, DateTime now);
}

public class BlockingService : IBlockingService
{
    private readonly BlockingConfig blockingConfig;

    private bool IsUse = false;
    private int[] Dates = new int[0];
    private int Hour = 8;

    public BlockingService(IOptions<BlockingConfig> config)
    {
        blockingConfig = config.Value;
    }

    public bool ChechAccess(DateTime inputDate)
    {
        if (IsUse == false)
        {
            return true;
        }

        var currentDate = DateTime.Now;
        inputDate = new DateTime(inputDate.Year, inputDate.Month, inputDate.Day, currentDate.Hour, currentDate.Minute, 0);

        var blockDates = new List<DateTime>();
        foreach (var item in Dates)
        {
            blockDates.Add(new DateTime(currentDate.Year, currentDate.Month, item, Hour, 0, 0));
        }

        foreach (var blockDate in blockDates)
        {
            if (blockDate.Date == inputDate.Date)
            {
                if (Hour <= currentDate.Hour)
                {
                    return false;
                }
            }

            if (blockDate >= inputDate)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsBlockedDay(DateTime calendarDay, DateTime now)
    {
        if (this.blockingConfig.IsUse == false)
        {
            return false;
        }

        var stopDate1 = new DateTime(now.Year, now.Month, 1, blockingConfig.Hour, 0, 0);
        var stopDate16 = new DateTime(now.Year, now.Month, 16, blockingConfig.Hour, 0, 0);

        if (now < stopDate1)
        {
            var d = new DateTime(stopDate1.Year, stopDate1.Month, 15).AddMonths(-1);
            if (calendarDay <= d)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (now >= stopDate1 && now < stopDate16)
        {
            var d = new DateTime(stopDate1.Year, stopDate1.Month, 1);
            if (calendarDay < d)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (now >= stopDate16)
        {
            var d = new DateTime(stopDate1.Year, stopDate1.Month, 16);
            if (calendarDay < d)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
}