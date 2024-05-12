﻿namespace Miratorg.TimeKeeper.BusinessLogic;

public  class TimeKeeperConverter
{
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
                PlanType = plan.PlanType
            };

            employee.Plans.Add(planDetail);
        }

        foreach (var scudInfoEntity in entity.ScudInfos)
        {
            var scudModel = new ScudInfoModel()
            {
                Begin = scudInfoEntity.Input,
                End = scudInfoEntity.Output
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

            employee.MountHours.Add(start, t.TotalHours);
            start = start.AddMonths(1);
        }

        return employee;
    }
}
