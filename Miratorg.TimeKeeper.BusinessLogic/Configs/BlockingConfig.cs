﻿using Microsoft.Extensions.Options;

namespace Miratorg.TimeKeeper.BusinessLogic.Configs;

public class BlockingConfig
{
    public bool IsUse { get; set; }
    public int Hour { get; set; }
}

public interface IBlockingService
{
    bool IsBlockedDay(DateTime calendarDay, DateTime now);
}

public class BlockingService : IBlockingService
{
    private readonly BlockingConfig blockingConfig;

    public BlockingService(IOptions<BlockingConfig> config)
    {
        blockingConfig = config.Value;
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