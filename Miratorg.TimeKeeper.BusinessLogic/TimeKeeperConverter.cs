namespace Miratorg.TimeKeeper.BusinessLogic;

public  class TimeKeeperConverter
{
    public static bool CheckInterval(DateTime currentTime, DateTime beginInterval, DateTime? endInterval)
    {
        if (endInterval == null)
        {
            return false;
        }

        if (beginInterval <= currentTime && currentTime <= endInterval)
        {
            return true;
        }

        return false;
    }

    public static EmployeeModel Convert(EmployeeEntity entity)
    {
        EmployeeModel employee = new EmployeeModel()
        {
            Id = entity.Id,
            StoreId = entity.StoreId,
            Name = entity.Name,
            Position = entity.Position,
            CodeNav = entity.CodeNav,

            Plans = new List<PlanDetailModel>(),
            ScudInfos = new List<ScudInfoModel>(),
            WorkDates = new List<Schedule1CPlanModel>(),
            MountHours = new Dictionary<DateTime, double>()
        };

        foreach (var plan in entity.Plans)
        {
            var planDetail = new PlanDetailModel()
            {
                Id = plan.Id,
                StoreId = plan.StoreId,
                Begin = plan.Begin,
                End = plan.End,
                PlanType = plan.PlanType,
                TypeOverWorkName = plan.TypeOverWork?.Name ?? "N/D"
            };

            employee.Plans.Add(planDetail);
        }

        foreach (var scudInfoEntity in entity.ScudInfos)
        {
            var scudModel = new ScudInfoModel()
            {
                Id = Guid.Empty,
                Begin = scudInfoEntity.Input,
                End = scudInfoEntity.Output,
                ScudInfoType = ScudInfoType.Scud
            };

            employee.ScudInfos.Add(scudModel);
        }

        foreach (var item in entity.ManualScuds)
        {
            var scudModel = new ScudInfoModel()
            {
                Id = item.Id,
                Begin = item.Input,
                End = item.Output,
                ScudInfoType = ScudInfoType.Manual
            };

            employee.ScudInfos.Add(scudModel);
        }

        if (entity.Schedule?.Dates != null)
        {
            foreach (var item in entity.Schedule.Dates)
            {
                employee.WorkDates.Add(new Schedule1CPlanModel()
                {
                    Begin = item.TimeBegin,
                    End = item.TimeEnd,
                });
            }
        }

        if (entity.Absences != null)
        {
            foreach (var item in entity.Absences)
            {
                employee.Absences.Add(new AbsenceModel()
                {
                    RepDate = item.RepDate,
                    Description = item.AbsenceDescription
                });
            }
        }


        // Подсчет часов в магазине за месяц //ToDo -  необходимо учитывать по магазинам
        DateTime start = new DateTime(2024, 1, 1);

        for (int i = 0; i < 100; i++)
        {
            TimeSpan t = TimeSpan.FromSeconds(0);
            var end = start.AddMonths(1);
            var plans = entity.Plans.Where(x => x.Begin >= start &&  x.End < end).ToList();

            foreach (var item in plans)
            {
                var time = item.End - item.Begin;
                t += time;
            }

            var workHours = t.TotalHours;
            if (workHours > 3)
            {
                workHours = workHours - 1;
            }

            employee.MountHours.Add(start, workHours);
            start = start.AddMonths(1);
        }

        return employee;
    }

    public static (double dayMinutes, double nightMinutes) CalculateDayAndNightMinutes(DateTime begin, DateTime end)
    {
        // Определение времени начала и конца дневного периода
        DateTime dayStart = new DateTime(begin.Year, begin.Month, begin.Day, 6, 0, 0);
        DateTime dayEnd = new DateTime(begin.Year, begin.Month, begin.Day, 22, 0, 0);

        double dayHours = 0.0;
        double nightHours = 0.0;

        if (end < dayStart)
        {
            // Весь интервал ночной
            nightHours = (end - begin).TotalMinutes;
        }
        else if (begin >= dayEnd)
        {
            // Весь интервал ночной
            nightHours = (end - begin).TotalMinutes;
        }
        else
        {
            // Начало дневного времени после начала интервала
            if (begin < dayStart)
            {
                nightHours += (dayStart - begin).TotalMinutes;
            }

            // Конец дневного времени перед концом интервала
            if (end > dayEnd)
            {
                nightHours += (end - dayEnd).TotalMinutes;
            }

            // Пересечение дневного времени с интервалом
            DateTime effectiveDayStart = begin > dayStart ? begin : dayStart;
            DateTime effectiveDayEnd = end < dayEnd ? end : dayEnd;
            dayHours += (effectiveDayEnd - effectiveDayStart).TotalMinutes;
        }

        return (dayHours, nightHours);
    }
}
