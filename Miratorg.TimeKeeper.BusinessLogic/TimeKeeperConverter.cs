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

    public static EmployeeModel Convert(EmployeeEntity employeeEntity)
    {
        EmployeeModel employee = new EmployeeModel()
        {
            Id = employeeEntity.Id,
            StoreId = employeeEntity.StoreId,
            Name = employeeEntity.Name,
            Position = employeeEntity.Position,
            CodeNav = employeeEntity.CodeNav,

            Plans = new List<PlanDetailModel>(),
            ScudInfos = new List<ScudInfoModel>(),
            WorkDates = new List<Schedule1CPlanModel>(),
            MountPlanUseHours = new Dictionary<DateTime, double>()
        };

        foreach (var plan in employeeEntity.Plans)
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

        foreach (var scudInfoEntity in employeeEntity.ScudInfos)
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

        foreach (var item in employeeEntity.ManualScuds)
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

        if (employeeEntity.Schedule?.Dates != null)
        {
            foreach (var item in employeeEntity.Schedule.Dates)
            {
                employee.WorkDates.Add(new Schedule1CPlanModel()
                {
                    Begin = item.TimeBegin,
                    End = item.TimeEnd,
                });
            }
        }

        if (employeeEntity.Absences != null)
        {
            foreach (var item in employeeEntity.Absences)
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
        for (DateTime currentMonth = start; currentMonth < start.AddMonths(100); currentMonth = currentMonth.AddMonths(1))
        {
            TimeSpan monthPlan = new TimeSpan();
            TimeSpan monthScud = new TimeSpan();

            for (DateTime currentDate = currentMonth; currentDate < currentMonth.AddMonths(1); currentDate = currentDate.AddDays(1))
            {
                // План + переработки
                var plans = employeeEntity.Plans.Where(x => x.Begin >= currentDate && x.End < currentDate.AddDays(1)).ToList();
                TimeSpan dayPlan = new TimeSpan();

                foreach (var item in plans)
                {
                    var time = item.End - item.Begin;
                    dayPlan += time;
                }

                if (dayPlan.TotalMinutes >= 180)
                {
                    dayPlan = dayPlan.Add(TimeSpan.FromHours(-1));
                }

                monthPlan += dayPlan;
                employee.DayPlanUseMinutes.Add(currentDate, dayPlan.TotalMinutes);

                // факт скуд + ручной скуд
                var scuds = employeeEntity.ScudInfos.Where(x => x.Input >= currentDate && x.Output < currentDate.AddDays(1)).ToList();
                var scudManuals = employeeEntity.ManualScuds.Where(x => x.Input >= currentDate && x.Output < currentDate.AddDays(1)).ToList();

                TimeSpan dayScud = new TimeSpan();

                foreach (var item in scuds)
                {
                    var time = item.Output - item.Input;
                    dayScud += time;
                }

                foreach (var item in scudManuals)
                {
                    var time = item.Output - item.Input;
                    dayScud += time;
                }

                if (dayScud.TotalMinutes >= 180)
                {
                    dayScud = dayScud.Add(TimeSpan.FromHours(-1));
                }

                monthScud += dayScud;
                employee.DayScudUseMinutes.Add(currentDate, dayScud.TotalMinutes);
            }

            employee.MountPlanUseHours.Add(currentMonth, monthPlan.TotalHours);
            employee.MountScudUseHours.Add(currentMonth, monthScud.TotalHours);
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
