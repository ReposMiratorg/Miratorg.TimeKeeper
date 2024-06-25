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

                PlanCalcBegin = plan.Begin,
                PlanCalcEnd = plan.End,
                
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
                var plans = employee.Plans.Where(x => x.OriginalBegin >= currentDate && x.OriginalEnd < currentDate.AddDays(1)).OrderBy(x => x.PlanCalcBegin).ToList();
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
                    var time = plan.PlanCalcEnd - plan.PlanCalcBegin;

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
                            plan.PlanCalcEnd = plan.PlanCalcEnd - TimeSpan.FromMinutes(timeObed30min);
                            plan.PlanObedTimeMinutes += timeObed30min;
                            timeObed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            timeObed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                            plan.PlanCalcEnd = plan.PlanCalcEnd - TimeSpan.FromMinutes(lunch);
                            plan.PlanObedTimeMinutes += lunch;
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
                            plan.PlanCalcEnd = plan.PlanCalcEnd - TimeSpan.FromMinutes(time2Obed30min);
                            plan.PlanObedTimeMinutes += time2Obed30min;
                            time2Obed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            time2Obed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                            plan.PlanCalcEnd = plan.PlanCalcEnd - TimeSpan.FromMinutes(lunch);
                            plan.PlanObedTimeMinutes += lunch;
                        }
                    }

                    var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(plan.PlanCalcBegin, plan.PlanCalcEnd);
                    //dayMinutes -= usedLunch;

                    ExportTime exportTime = new ExportTime()
                    {
                        Date = new DateOnly(plan.PlanCalcBegin.Year, plan.PlanCalcBegin.Month, plan.PlanCalcBegin.Day),
                        Begin = plan.PlanCalcBegin,
                        End = plan.PlanCalcEnd,
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

    public static List<(DateTime Start, DateTime End)> SplitTimePeriod(DateTime start, DateTime end)
    {
        List<(DateTime Start, DateTime End)> result = new List<(DateTime Start, DateTime End)>();

        // Define time intervals
        DateTime startOfDay = start.Date;
        DateTime endOfDay = start.Date.AddDays(1).AddSeconds(-1);

        DateTime period1Start = startOfDay.AddHours(0);
        DateTime period1End = startOfDay.AddHours(6);
        DateTime period2Start = startOfDay.AddHours(6);
        DateTime period2End = startOfDay.AddHours(22);
        DateTime period3Start = startOfDay.AddHours(22);
        DateTime period3End = endOfDay;

        // Check and split periods
        if (start < period1End && end > period1Start)
        {
            result.Add((
                Start: start > period1Start ? start : period1Start,
                End: end < period1End ? end : period1End
            ));
        }

        if (start < period2End && end > period2Start)
        {
            result.Add((
                Start: start > period2Start ? start : period2Start,
                End: end < period2End ? end : period2End
            ));
        }

        if (start < period3End && end > period3Start)
        {
            result.Add((
                Start: start > period3Start ? start : period3Start,
                End: end < period3End ? end : period3End
            ));
        }

        return result;
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

        var plans = new List<PlanEntity>();

        foreach (var item in employeeEntity.Plans)
        {
            var resultCheck = SplitTimePeriod(item.Begin, item.End);
            foreach (var plan in resultCheck)
            {
                plans.Add(new PlanEntity()
                {
                    Begin = plan.Start,
                    End = plan.End,
                    CustomTypeWorkId = item.CustomTypeWorkId,
                    TypeOverWorkId = item.TypeOverWorkId,
                    EmployeeId = item.EmployeeId,
                    Id = item.Id,
                    PlanType = item.PlanType,
                    StoreId = item.StoreId,
                    CustomTypeWork = item.CustomTypeWork,
                    Employee = item.Employee,
                    TypeOverWork = item.TypeOverWork
                });
            }
        }

        for (int i = 0; i < plans.Count; i++)
        {
            //ToDo - добавить время отдыха за прошлый день (конец дня)

            var plan = plans[i];

            var planDetail = new PlanDetailModel()
            {
                Id = plan.Id,
                StoreId = plan.StoreId,
                OriginalBegin = plan.Begin,
                OriginalEnd = plan.End,
                PlanCalcBegin = plan.Begin,
                PlanCalcEnd = plan.End,
                FactCalcBegin = plan.Begin,
                FactCalcEnd = plan.End,
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
                    var sigurInput = sigurEvents.Where(x => x.EventTime >= plan.Begin.AddMinutes(-180) && x.EventTime <= plan.Begin.AddMinutes(60)).OrderBy(x => x.EventTime).FirstOrDefault()?.EventTime;

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
                    var sigurInput = sigurEvents.Where(x => x.EventTime >= plan.Begin.AddMinutes(-180) && x.EventTime <= plan.Begin.AddMinutes(60)).OrderBy(x => x.EventTime).FirstOrDefault()?.EventTime;
                    var sigurOutput = sigurEvents.Where(x => x.EventTime >= plan.End.AddMinutes(-60) && x.EventTime <= plan.End.AddMinutes(180)).OrderByDescending(x => x.EventTime).FirstOrDefault()?.EventTime;

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

        // удаляем отметки если были
        DateTime start = new DateTime(2024, 1, 1);
        for (DateTime date = start; date < start.AddMonths(100); date = date.AddDays(1))
        {
            var scuds = employee.ScudInfos.Where(x => x.Begin.Date == date && x.ScudInfoType == ScudInfoType.Scud).ToList();
            var manuals = employee.ScudInfos.Where(x => x.Begin.Date == date && x.ScudInfoType == ScudInfoType.Manual).ToList();

            if (manuals.Count > 0 && scuds.Count > 0)
            {
                foreach (var item in scuds)
                {
                    employee.ScudInfos.Remove(item);
                }
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
                var plans = employee.Plans.Where(x => x.OriginalBegin >= currentDate && x.OriginalEnd < currentDate.AddDays(1)).OrderBy(x => x.PlanCalcBegin).ToList();
                var scuds = employee.ScudInfos.Where(x => x.Begin >= currentDate && x.End < currentDate.AddDays(1)).ToList();

                //Время полного дня (все запланированное время без перерыва)
                TimeSpan dayPlanFill = new TimeSpan();

                //Время полного дня (все Фактическое время без перерыва)
                TimeSpan dayFactFill = new TimeSpan();

                //Чистое время работы без перерывов
                TimeSpan dayPlanClear = new TimeSpan();

                // Чистое время для (факт без пееррыва)
                TimeSpan dayFactClear = new TimeSpan();

                // Обед 30 минут между 4м и 8м часом работы
                bool obed30min = false;
                int timeObed30min = 30;

                // Обед 60 минут после 8го часа работы
                bool obed60min = false;
                int time2Obed30min = 30;


                // Обед 30 минут между 4м и 8м часом работы
                bool _fact_obed30min = false;
                int _fact_timeObed30min = 30;

                // Обед 60 минут после 8го часа работы
                bool _fact_obed60min = false;
                int _fact_time2Obed30min = 30;

                List<ExportTime> exportTimes = new List<ExportTime>();
                TimeSpan dayScud = new TimeSpan();

                // расчитываем план
                for (int i = 0; i < plans.Count; i++)
                {
                    var plan = plans[i];
                    var time = plan.PlanCalcEnd - plan.PlanCalcBegin;

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
                            plan.PlanCalcEnd = plan.PlanCalcEnd - TimeSpan.FromMinutes(timeObed30min);
                            plan.PlanObedTimeMinutes += timeObed30min;
                            timeObed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            timeObed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                            plan.PlanCalcEnd = plan.PlanCalcEnd - TimeSpan.FromMinutes(lunch);
                            plan.PlanObedTimeMinutes += lunch;
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
                            plan.PlanCalcEnd = plan.PlanCalcEnd - TimeSpan.FromMinutes(time2Obed30min);
                            plan.PlanObedTimeMinutes += time2Obed30min;
                            time2Obed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            time2Obed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                            plan.PlanCalcEnd = plan.PlanCalcEnd - TimeSpan.FromMinutes(lunch);
                            plan.PlanObedTimeMinutes += lunch;
                        }
                    }

                    var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(plan.PlanCalcBegin, plan.PlanCalcEnd);
                    //dayMinutes -= usedLunch;

                    ExportTime exportTime = new ExportTime()
                    {
                        Date = new DateOnly(plan.PlanCalcBegin.Year, plan.PlanCalcBegin.Month, plan.PlanCalcBegin.Day),
                        Begin = plan.PlanCalcBegin,
                        End = plan.PlanCalcEnd,
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

                monthPlan += dayPlanClear;

                employee.DayPlanUseMinutes.Add(currentDate, dayPlanClear.TotalMinutes);

                // расчитываем факт
                for (int i = 0; i < plans.Count; i++)
                {
                    var plan = plans[i];

                    if (scuds.Count == 0)
                    {
                        var fact = new ExportTimeFact()
                        {
                            Begin = plan.FactCalcBegin,
                            End = plan.FactCalcEnd,
                            Date = new DateOnly(plan.FactCalcBegin.Date.Year, plan.FactCalcBegin.Date.Month, plan.FactCalcBegin.Date.Day),
                            WorkTime = plan.WorkTime,

                            DayMinutes = 0,
                            NightMinutes = 0
                        };

                        employee.ExportFactTimes.Add(fact);
                        continue;
                    }

                    var min = scuds.MinBy(x => x.Begin).Begin;
                    var max = scuds.MaxBy(x => x.End).End;

                    // корректируем время
                    plan.FactCalcBegin = plan.FactCalcBegin > min ? plan.FactCalcBegin : min;
                    plan.FactCalcEnd = plan.FactCalcEnd < max ? plan.FactCalcEnd : max;

                    var time = plan.FactCalcEnd - plan.FactCalcBegin;

                    dayFactFill += time;

                    int usedLunch = 0;

                    if (_fact_obed30min == false && dayFactFill > TimeSpan.FromHours(4))
                    {
                        var t0 = dayFactFill - TimeSpan.FromHours(4) - TimeSpan.FromMinutes(_fact_timeObed30min);
                        if (t0.TotalMinutes >= 0) // если время больше чем 4 часа - убираем обед
                        {
                            time -= TimeSpan.FromMinutes(_fact_timeObed30min);
                            usedLunch += _fact_timeObed30min;
                            _fact_obed30min = true;
                            plan.FactCalcEnd = plan.FactCalcEnd - TimeSpan.FromMinutes(_fact_timeObed30min);
                            plan.FactObedTimeMinutes += _fact_timeObed30min;
                            _fact_timeObed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            _fact_timeObed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                            plan.FactCalcEnd = plan.FactCalcEnd - TimeSpan.FromMinutes(lunch);
                            plan.FactObedTimeMinutes += lunch;
                        }
                    }

                    if (_fact_obed60min == false && dayFactFill > TimeSpan.FromHours(8))
                    {
                        var t0 = dayFactFill - TimeSpan.FromHours(8) - TimeSpan.FromMinutes(_fact_time2Obed30min);
                        if (t0.TotalMinutes >= 0) // если время больше чем 8 часа- убираем обед
                        {
                            time -= TimeSpan.FromMinutes(_fact_time2Obed30min);
                            usedLunch += _fact_time2Obed30min;
                            obed60min = true;
                            plan.FactCalcEnd = plan.FactCalcEnd - TimeSpan.FromMinutes(_fact_time2Obed30min);
                            plan.FactObedTimeMinutes += _fact_time2Obed30min;
                            _fact_time2Obed30min = 0;
                        }
                        else
                        {
                            int lunch = ((int)t0.TotalMinutes) * -1;
                            _fact_time2Obed30min -= lunch;
                            time -= TimeSpan.FromMinutes(lunch);
                            usedLunch += lunch;
                            plan.FactCalcEnd = plan.FactCalcEnd - TimeSpan.FromMinutes(lunch);
                            plan.FactObedTimeMinutes += lunch;
                        }
                    }

                    var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightMinutes(plan.FactCalcBegin, plan.FactCalcEnd);
                    //dayMinutes -= usedLunch;

                    ExportTime fact1 = new ExportTime()
                    {
                        Date = new DateOnly(plan.FactCalcBegin.Year, plan.FactCalcBegin.Month, plan.FactCalcBegin.Day),
                        Begin = plan.FactCalcBegin,
                        End = plan.FactCalcEnd,
                        DayMinutes = dayMinutes,
                        NightMinutes = nightMinutes,
                        WorkTime = plan.WorkTime
                    };

                    dayScud += TimeSpan.FromMinutes(dayMinutes + nightMinutes);

                    employee.ExportFactTimes.Add(fact1);

                    dayFactClear += time;
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
