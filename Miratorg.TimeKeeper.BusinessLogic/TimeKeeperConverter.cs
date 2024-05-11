namespace Miratorg.TimeKeeper.BusinessLogic;

public  class TimeKeeperConverter
{
    public static EmployeeModel Convert(EmployeeEntity entity)
    {
        EmployeeModel employee = new EmployeeModel()
        {
            EmployeeId = entity.Id,
            StoreId = entity.StoreId,
            Name = entity.Name,
            Plans = new List<PlanDetailModel>(),
            ScudInfos = new List<ScudInfoModel>(),
            WorkDates = new List<Schedule1CPlanModel>()
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

        return employee;
    }
}
