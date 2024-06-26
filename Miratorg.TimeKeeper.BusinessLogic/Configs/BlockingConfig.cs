﻿using Microsoft.Extensions.Configuration;

namespace Miratorg.TimeKeeper.BusinessLogic.Configs;

public interface IBlockingConfig
{
    bool IsUse { get; }
    int[] Dates { get; }
    int Hour { get; }

    bool ChechAccess(DateTime date);
}

public class BlockingConfig : IBlockingConfig
{
    public BlockingConfig(IConfiguration configuration)
    {
        configuration.GetSection(nameof(BlockingConfig)).Bind(this);
    }

    public bool IsUse { get; set; }

    public int[] Dates { get; set; }

    public int Hour { get; set; }

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
}