using System;

namespace Miratorg.TimeKeeper.BusinessLogic;

public class TimeKeeperConverter
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

    public static EmployeeModel ConvertV3(EmployeeEntity employeeEntity, List<SigurEventModel> sigurEvents)
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
            MountPlanUseMinuts = new Dictionary<DateTime, double>(),
            ExportPlanTimes = new List<ExportTime>(),
            ExportFactTimes = new List<ExportTime>()
        };

        for (int i = 0; i < employeeEntity.Plans.Count; i++)
        {
            //ToDo - добавть время отдыха за прошлый день (конец дня)

            var plan = employeeEntity.Plans[i];

            var planDetail = new PlanDetailModel()
            {
                Id = plan.Id,
                StoreId = plan.StoreId,
                OriginalBegin = plan.Begin,
                OriginalEnd = plan.End,
                CalcBegin = plan.Begin,
                CalcEnd = plan.End,
                PlanType = plan.PlanType
            };

            if (planDetail.PlanType == PlanType.Plan)
            {
                planDetail.WorkTime = "regular";
            }
            else
            {
                planDetail.WorkTime = plan.TypeOverWork?.Code ?? "N/D";
            }

            employee.Plans.Add(planDetail);

            if (plan.Begin.Hour == 0 && plan.Begin.Minute == 0) // Время входа засчитывается если нет точного времени
            {
                var sigurOutput = sigurEvents.Where(x => x.EventTime >= plan.End && x.EventTime <= plan.End.AddMinutes(180))
                    .OrderByDescending(x => x.EventTime).FirstOrDefault()?.EventTime;

                if (sigurOutput != null) //если сотрудник был на работе
                {
                    var scudInfo = new ScudInfoModel
                    {
                        ScudInfoType = ScudInfoType.Scud,
                        Begin = plan.Begin,
                        End = (DateTime)sigurOutput
                    };

                    employee.ScudInfos.Add(scudInfo);
                }
            }
            else
            {
                if (plan.End.Hour == 23 && plan.End.Minute == 59)
                {
                    var sigurInput = sigurEvents.Where(x => x.EventTime >= plan.Begin.AddMinutes(-180) && x.EventTime <= plan.Begin.AddMinutes(60))
                        .OrderBy(x => x.EventTime).FirstOrDefault()?.EventTime;

                    if (sigurInput != null) //Если есть вход
                    {
                        var scudInfo = new ScudInfoModel
                        {
                            ScudInfoType = ScudInfoType.Scud,
                            Begin = (DateTime)sigurInput,
                            End = plan.End
                        };

                        employee.ScudInfos.Add(scudInfo);
                    }
                }
                else
                {
                    var sigurInput = sigurEvents.Where(x => x.EventTime >= plan.Begin.AddMinutes(-180) && x.EventTime <= plan.Begin.AddMinutes(60))
                        .OrderBy(x => x.EventTime).FirstOrDefault()?.EventTime;

                    var sigurOutput = sigurEvents.Where(x => x.EventTime >= plan.End.AddMinutes(-60) && x.EventTime <= plan.End.AddMinutes(180))
                        .OrderByDescending(x => x.EventTime).FirstOrDefault()?.EventTime;

                    if (sigurInput != null && sigurOutput != null)
                    {
                        var scudInfo = new ScudInfoModel()
                        {
                            Begin = (DateTime)sigurInput,
                            End = plan.End,
                            ScudInfoType = ScudInfoType.Scud
                        };

                        employee.ScudInfos.Add(scudInfo);
                    }
                }
            }
        }

        // this logic not used more

        //foreach (var scudInfoEntity in employeeEntity.ScudInfos)
        //{
        //    var scudModel = new ScudInfoModel()
        //    {
        //        Id = Guid.Empty,
        //        Begin = scudInfoEntity.Input,
        //        End = scudInfoEntity.Output,
        //        ScudInfoType = ScudInfoType.Scud
        //    };

        //    employee.ScudInfos.Add(scudModel);
        //}

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
                var plans = employee.Plans.Where(x => x.OriginalBegin >= currentDate && x.OriginalEnd < currentDate.AddDays(1)).OrderBy(x => x.CalcBegin).ToList();
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
                int time2Obed30min = 30;

                List<ExportTime> exportTimes = new List<ExportTime>();

                for (int i = 0; i < plans.Count; i++)
                {
                    var plan = plans[i];
                    var time = plan.CalcEnd - plan.CalcBegin;

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
                            plan.CalcEnd = plan.CalcEnd - TimeSpan.FromMinutes(timeObed30min);
                            plan.ObedTimeMinutes += timeObed30min;
                            timeObed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            timeObed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                            plan.CalcEnd = plan.CalcEnd - TimeSpan.FromMinutes(lunch);
                            plan.ObedTimeMinutes += lunch;
                        }
                    }

                    if (obed60min == false && dayPlanFill > TimeSpan.FromHours(8))
                    {
                        var t0 = dayPlanFill - TimeSpan.FromHours(8) - TimeSpan.FromMinutes(time2Obed30min);
                        if (t0.TotalMinutes >= 0) // если время больше чем 8 часа- убираем обед
                        {
                            time -= TimeSpan.FromMinutes(time2Obed30min);
                            usedLunch += time2Obed30min;
                            obed60min = true;
                            plan.CalcEnd = plan.CalcEnd - TimeSpan.FromMinutes(time2Obed30min);
                            plan.ObedTimeMinutes += time2Obed30min;
                            time2Obed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            time2Obed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                            plan.CalcEnd = plan.CalcEnd - TimeSpan.FromMinutes(lunch);
                            plan.ObedTimeMinutes += lunch;
                        }
                    }

                    var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(plan.CalcBegin, plan.CalcEnd);
                    //dayMinutes -= usedLunch;

                    ExportTime exportTime = new ExportTime()
                    {
                        Date = new DateOnly(plan.CalcBegin.Year, plan.CalcBegin.Month, plan.CalcBegin.Day),
                        Begin = plan.CalcBegin,
                        End = plan.CalcEnd,
                        DayMinutes = dayMinutes,
                        NightMinutes = nightMinutes
                    };

                    if (plan.PlanType == PlanType.Plan)
                    {
                        exportTime.WorkTime = "regular";
                    }
                    else
                    {
                        exportTime.WorkTime = plan.WorkTime ?? "N/D";
                    }

                    plan.CaclWorkTimeMinutes = dayMinutes + nightMinutes;
                    plan.CalcWorkDayMinutes = dayMinutes;
                    plan.CalcWorkNightMinutes = nightMinutes;

                    exportTimes.Add(exportTime);

                    dayPlanClear += time;
                }

                // факт скуд + ручной скуд
                TimeSpan dayScud = new TimeSpan();
                //ToDo - need release

                monthPlan += dayPlanClear;

                employee.DayPlanUseMinutes.Add(currentDate, dayPlanClear.TotalMinutes);

                foreach (var planTime in exportTimes)
                {
                    var fact = new ExportTimeFact()
                    {
                        Begin = planTime.Begin,
                        End = planTime.End,
                        Date = planTime.Date,
                        WorkTime = planTime.WorkTime
                    };

                    var day = new DateTime(fact.Date.Year, fact.Date.Month, fact.Date.Day);
                    var scudInfos = employee.ScudInfos.Where(x => x.Begin.Date == day).ToList();

                    if (scudInfos.Count == 0)
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
                        var scudBegin = scudInfos.Min(x => x.Begin);
                        var scudEnd = scudInfos.Max(x => x.End);

                        DateTime actualStart = scudBegin < fact.Begin ? fact.Begin : scudBegin;
                        DateTime actualEnd = scudEnd > fact.End ? fact.End : scudEnd;

                        if (actualStart >= fact.End || actualEnd <= fact.Begin)
                        {
                            fact.DayMinutes = 0;
                            fact.NightMinutes = 0;
                        }
                        else
                        {
                            var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(actualStart, actualEnd);
                            // Вычисляем длительность пребывания на работе в минутах
                            fact.DayMinutes = planTime.DayMinutes < dayMinutes ? planTime.DayMinutes : dayMinutes;
                            fact.NightMinutes = planTime.NightMinutes < nightMinutes ? planTime.NightMinutes : nightMinutes;

                            dayScud += TimeSpan.FromMinutes(dayMinutes + nightMinutes);
                        }
                    }

                    employee.ExportFactTimes.Add(fact);
                }

                employee.ExportPlanTimes.AddRange(exportTimes);
                employee.DayScudUseMinutes.Add(currentDate, dayScud.TotalMinutes);

                monthScud += dayScud;
            }

            employee.MountPlanUseMinuts.Add(currentMonth, monthPlan.TotalMinutes);
            employee.MountScudUseMinutes.Add(currentMonth, monthScud.TotalMinutes);
        }

        return employee;
    }

    private static EmployeeModel PrepareForCalc(EmployeeEntity employeeEntity, List<SigurEventModel> sigurEvents)
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
            MountPlanUseMinuts = new Dictionary<DateTime, double>(),
            ExportPlanTimes = new List<ExportTime>(),
            ExportFactTimes = new List<ExportTime>()
        };

        for (int i = 0; i < employeeEntity.Plans.Count; i++)
        {
            //ToDo - добавить время отдыха за прошлый день (конец дня)

            var plan = employeeEntity.Plans[i];

            var planDetail = new PlanDetailModel()
            {
                Id = plan.Id,
                StoreId = plan.StoreId,
                OriginalBegin = plan.Begin,
                OriginalEnd = plan.End,
                CalcBegin = plan.Begin,
                CalcEnd = plan.End,
                PlanType = plan.PlanType
            };

            if (planDetail.PlanType == PlanType.Plan)
            {
                planDetail.WorkTime = "regular";
            }
            else
            {
                planDetail.WorkTime = plan.TypeOverWork?.Code ?? "N/D";
            }

            employee.Plans.Add(planDetail);

            if (plan.Begin.Hour == 0 && plan.Begin.Minute == 0) // Время входа засчитывается если нет точного времени
            {
                var sigurOutput = sigurEvents.Where(x => x.EventTime >= plan.End && x.EventTime <= plan.End.AddMinutes(180))
                    .OrderByDescending(x => x.EventTime).FirstOrDefault()?.EventTime;

                if (sigurOutput != null) //если сотрудник был на работе
                {
                    var scudInfo = new ScudInfoModel
                    {
                        ScudInfoType = ScudInfoType.Scud,
                        Begin = plan.Begin,
                        End = (DateTime)sigurOutput
                    };

                    employee.ScudInfos.Add(scudInfo);
                }
            }
            else
            {
                if (plan.End.Hour == 23 && plan.End.Minute == 59)
                {
                    var sigurInput = sigurEvents.Where(x => x.EventTime >= plan.Begin.AddMinutes(-180) && x.EventTime <= plan.Begin.AddMinutes(60))
                        .OrderBy(x => x.EventTime).FirstOrDefault()?.EventTime;

                    if (sigurInput != null) //Если есть вход
                    {
                        var scudInfo = new ScudInfoModel
                        {
                            ScudInfoType = ScudInfoType.Scud,
                            Begin = (DateTime)sigurInput,
                            End = plan.End
                        };

                        employee.ScudInfos.Add(scudInfo);
                    }
                }
                else
                {
                    var sigurInput = sigurEvents.Where(x => x.EventTime >= plan.Begin.AddMinutes(-180) && x.EventTime <= plan.Begin.AddMinutes(60))
                        .OrderBy(x => x.EventTime).FirstOrDefault()?.EventTime;

                    var sigurOutput = sigurEvents.Where(x => x.EventTime >= plan.End.AddMinutes(-60) && x.EventTime <= plan.End.AddMinutes(180))
                        .OrderByDescending(x => x.EventTime).FirstOrDefault()?.EventTime;

                    if (sigurInput != null && sigurOutput != null)
                    {
                        var scudInfo = new ScudInfoModel()
                        {
                            Begin = (DateTime)sigurInput,
                            End = plan.End,
                            ScudInfoType = ScudInfoType.Scud
                        };

                        employee.ScudInfos.Add(scudInfo);
                    }
                }
            }
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

        return employee;
    }

    public static EmployeeModel ConvertV4(EmployeeEntity employeeEntity, List<SigurEventModel> sigurEvents)
    {
        var employee = PrepareForCalc(employeeEntity, sigurEvents);

        // Подсчет часов в магазине за месяц //ToDo -  необходимо учитывать по магазинам
        DateTime start = new DateTime(2024, 1, 1);
        for (DateTime currentMonth = start; currentMonth < start.AddMonths(100); currentMonth = currentMonth.AddMonths(1))
        {
            TimeSpan monthPlan = new TimeSpan();
            TimeSpan monthScud = new TimeSpan();

            for (DateTime currentDate = currentMonth; currentDate < currentMonth.AddMonths(1); currentDate = currentDate.AddDays(1))
            {
                // План + переработки
                var plans = employee.Plans.Where(x => x.OriginalBegin >= currentDate && x.OriginalEnd < currentDate.AddDays(1)).OrderBy(x => x.CalcBegin).ToList();
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
                int time2Obed30min = 30;

                List<ExportTime> exportTimes = new List<ExportTime>();

                for (int i = 0; i < plans.Count; i++)
                {
                    var plan = plans[i];
                    var time = plan.CalcEnd - plan.CalcBegin;

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
                            plan.CalcEnd = plan.CalcEnd - TimeSpan.FromMinutes(timeObed30min);
                            plan.ObedTimeMinutes += timeObed30min;
                            timeObed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            timeObed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                            plan.CalcEnd = plan.CalcEnd - TimeSpan.FromMinutes(lunch);
                            plan.ObedTimeMinutes += lunch;
                        }
                    }

                    if (obed60min == false && dayPlanFill > TimeSpan.FromHours(8))
                    {
                        var t0 = dayPlanFill - TimeSpan.FromHours(8) - TimeSpan.FromMinutes(time2Obed30min);
                        if (t0.TotalMinutes >= 0) // если время больше чем 8 часа- убираем обед
                        {
                            time -= TimeSpan.FromMinutes(time2Obed30min);
                            usedLunch += time2Obed30min;
                            obed60min = true;
                            plan.CalcEnd = plan.CalcEnd - TimeSpan.FromMinutes(time2Obed30min);
                            plan.ObedTimeMinutes += time2Obed30min;
                            time2Obed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            time2Obed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                            plan.CalcEnd = plan.CalcEnd - TimeSpan.FromMinutes(lunch);
                            plan.ObedTimeMinutes += lunch;
                        }
                    }

                    var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(plan.CalcBegin, plan.CalcEnd);
                    //dayMinutes -= usedLunch;

                    ExportTime exportTime = new ExportTime()
                    {
                        Date = new DateOnly(plan.CalcBegin.Year, plan.CalcBegin.Month, plan.CalcBegin.Day),
                        Begin = plan.CalcBegin,
                        End = plan.CalcEnd,
                        DayMinutes = dayMinutes,
                        NightMinutes = nightMinutes,
                        WorkTime = plan.WorkTime
                    };

                    plan.CaclWorkTimeMinutes = dayMinutes + nightMinutes;
                    plan.CalcWorkDayMinutes = dayMinutes;
                    plan.CalcWorkNightMinutes = nightMinutes;

                    exportTimes.Add(exportTime);

                    dayPlanClear += time;
                }

                // факт скуд + ручной скуд
                TimeSpan dayScud = new TimeSpan();
                //ToDo - need release

                monthPlan += dayPlanClear;

                employee.DayPlanUseMinutes.Add(currentDate, dayPlanClear.TotalMinutes);

                foreach (var planTime in exportTimes)
                {
                    var fact = new ExportTimeFact()
                    {
                        Begin = planTime.Begin,
                        End = planTime.End,
                        Date = planTime.Date,
                        WorkTime = planTime.WorkTime
                    };

                    var day = new DateTime(fact.Date.Year, fact.Date.Month, fact.Date.Day);
                    var scudInfos = employee.ScudInfos.Where(x => x.Begin.Date == day).ToList();

                    if (scudInfos.Count == 0)
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
                        var scudBegin = scudInfos.Min(x => x.Begin);
                        var scudEnd = scudInfos.Max(x => x.End);

                        DateTime actualStart = scudBegin < fact.Begin ? fact.Begin : scudBegin;
                        DateTime actualEnd = scudEnd > fact.End ? fact.End : scudEnd;

                        if (actualStart >= fact.End || actualEnd <= fact.Begin)
                        {
                            fact.DayMinutes = 0;
                            fact.NightMinutes = 0;
                        }
                        else
                        {
                            var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(actualStart, actualEnd);
                            // Вычисляем длительность пребывания на работе в минутах
                            fact.DayMinutes = planTime.DayMinutes < dayMinutes ? planTime.DayMinutes : dayMinutes;
                            fact.NightMinutes = planTime.NightMinutes < nightMinutes ? planTime.NightMinutes : nightMinutes;

                            dayScud += TimeSpan.FromMinutes(dayMinutes + nightMinutes);
                        }
                    }

                    employee.ExportFactTimes.Add(fact);
                }

                employee.ExportPlanTimes.AddRange(exportTimes);
                employee.DayScudUseMinutes.Add(currentDate, dayScud.TotalMinutes);

                monthScud += dayScud;
            }

            employee.MountPlanUseMinuts.Add(currentMonth, monthPlan.TotalMinutes);
            employee.MountScudUseMinutes.Add(currentMonth, monthScud.TotalMinutes);
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
