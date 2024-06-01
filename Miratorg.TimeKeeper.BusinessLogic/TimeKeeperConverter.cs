using Microsoft.Extensions.Logging;
using Miratorg.TimeKeeper.BusinessLogic.Models;
using System;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                TypeOverWorkName = plan.TypeOverWork?.Code ?? "N/D"
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

                if (dayPlan.TotalMinutes >= 240)
                {
                    dayPlan = dayPlan.Add(TimeSpan.FromHours(-1));
                }

                int overtime = plans.Sum(e => (int)(e.End - e.Begin).TotalMinutes);

                if (overtime > 8 * 60)
                {
                    dayPlan = dayPlan.Add(TimeSpan.FromMinutes(60));
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

                if (dayScud.TotalMinutes >= 240)
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

    public static EmployeeModel ConvertV2(EmployeeEntity employeeEntity)
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
            MountPlanUseHours = new Dictionary<DateTime, double>(),
            ExportPlanTimes = new List<ExportTime>(),
            ExportFactTimes = new List<ExportTime>()
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
                TypeOverWorkName = plan.TypeOverWork?.Code ?? "N/D"
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
                var plans = employeeEntity.Plans.Where(x => x.Begin >= currentDate && x.End < currentDate.AddDays(1)).OrderBy(x => x.Begin).ToList();
                var scuds = employeeEntity.ScudInfos.Where(x => x.Input >= currentDate && x.Output < currentDate.AddDays(1)).OrderBy(x => x.Input).ToList();
                var scudManuals = employeeEntity.ManualScuds.Where(x => x.Input >= currentDate && x.Output < currentDate.AddDays(1)).OrderBy(x => x.Input).ToList();

                //Время полного дня (все запланированное время без перерыва)
                TimeSpan dayPlanFill = new TimeSpan();

                //Чистое время работы без перерывов
                TimeSpan dayPlanClear = new TimeSpan();

                // Обед 30 минут между 4м и 8м часом работы
                bool obed30min = false;
                int timeObed30min = 30;

                // Обед 60 минут после 8го часа работы
                bool obed60min = false;
                int timeObed60min = 60;

                for (int i = 0; i < plans.Count; i++)
                {
                    var plan = plans[i];
                    var time = plan.End - plan.Begin;

                    dayPlanFill += time;

                    int usedLunch = 0;

                    if (obed30min == false && dayPlanFill > TimeSpan.FromHours(4))
                    {
                        var t0 = dayPlanFill - TimeSpan.FromHours(4) - TimeSpan.FromMinutes(timeObed30min);
                        if (t0.TotalMinutes >= 0) // если время больше чем 4 часа - убираем обед
                        {
                            time -= TimeSpan.FromMinutes(timeObed30min);
                            usedLunch += timeObed30min;
                            obed30min = true;
                            timeObed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            timeObed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                        }
                    }

                    if (obed60min == false && dayPlanFill > TimeSpan.FromHours(8))
                    {
                        var t0 = dayPlanFill - TimeSpan.FromHours(8) - TimeSpan.FromMinutes(timeObed60min);
                        if (t0.TotalMinutes >= 0) // если время больше чем 8 часа- убираем обед
                        {
                            time -= TimeSpan.FromMinutes(timeObed60min);
                            usedLunch += timeObed60min;
                            obed60min = true;
                            timeObed60min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            timeObed60min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch); ;
                            usedLunch += lunch;
                        }
                    }

                    var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(plan.Begin, plan.End);
                    dayMinutes -= usedLunch;

                    ExportTime exportTime = new ExportTime()
                    {
                        Date = new DateOnly(plan.Begin.Year, plan.Begin.Month, plan.Begin.Day),
                        Begin = plan.Begin,
                        End = plan.End,
                        DayMinutes = dayMinutes,
                        NightMinutes = nightMinutes
                    };

                    if (plan.PlanType == PlanType.Plan)
                    {
                        exportTime.WorkTime = "regular";
                    }
                    else
                    {
                        exportTime.WorkTime = plan.TypeOverWork?.Code ?? "N/D";
                    }

                    employee.ExportPlanTimes.Add(exportTime);

                    dayPlanClear += time;
                }

                // факт скуд + ручной скуд
                TimeSpan dayScud = new TimeSpan();

                monthPlan += dayPlanClear;
                monthScud += dayScud;

                employee.DayPlanUseMinutes.Add(currentDate, dayPlanClear.TotalMinutes);
                employee.DayScudUseMinutes.Add(currentDate, dayScud.TotalMinutes);

                foreach (var planTime in employee.ExportPlanTimes)
                {
                    var fact = new ExportTimeFact()
                    {
                        Begin = planTime.Begin,
                        End = planTime.End,
                        Date = planTime.Date,
                        WorkTime = planTime.WorkTime
                    };

                    var day = new DateTime(fact.Date.Year, fact.Date.Month, fact.Date.Day);
                    var scud = employee.ScudInfos.Where(x => x.Begin.Date == day).ToList();

                    if (scud.Count == 0)
                    {
                        // нет фактоы явки
                        fact.DayMinutes = 0;
                        fact.NightMinutes = 0;
                    }
                    else
                    {
                        /*
                        workStart – начало рабочего дня
                        workEnd – конец рабочего дня
                        arrival – фактическое время прихода на работу
                        departure – фактическое время ухода с работы
                         */
                        var scudBegin = scud.Min(x => x.Begin);
                        var scudEnd = scud.Max(x => x.End);

                        DateTime actualStart = scudBegin < fact.Begin ? fact.Begin : scudBegin;
                        DateTime actualEnd = scudEnd > fact.End ? fact.End : scudEnd;

                        if (actualStart >= fact.End || actualEnd <= fact.Begin)
                        {
                            //Console.WriteLine("Время пребывания на работе: 0 минут");
                            fact.DayMinutes = 0;
                            fact.NightMinutes = 0;
                        }
                        else
                        {
                            var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(actualStart, actualEnd);
                            // Вычисляем длительность пребывания на работе в минутах
                            //TimeSpan duration = actualEnd - actualStart;
                            //Console.WriteLine($"Время пребывания на работе: {duration.TotalMinutes} минут");
                            fact.DayMinutes = planTime.DayMinutes < dayMinutes ? planTime.DayMinutes : dayMinutes;
                            fact.NightMinutes = planTime.NightMinutes < nightMinutes ? planTime.NightMinutes : nightMinutes;
                        }
                    }

                    employee.ExportFactTimes.Add(fact);
                }
            }

            employee.MountPlanUseHours.Add(currentMonth, monthPlan.TotalHours);
            employee.MountScudUseHours.Add(currentMonth, monthScud.TotalHours);
        }

        return employee;
    }

    public static (int dayMinutes, int nightMinutes) CalculateDayAndNightMinutes(DateTime begin, DateTime end)
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

        return ((int)dayHours, (int)nightHours);
    }
}

public static class EnumerableExtensions
{
    public static T? MinByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where T : class
    {
        if (source == null || !source.Any())
        {
            return null;
        }

        return source.MinBy(selector);
    }
}
