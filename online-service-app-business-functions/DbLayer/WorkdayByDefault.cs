using System;
using System.Collections.Generic;

namespace online_service_app_business_functions.DbLayer;

public partial class WorkdayByDefault
{
    public WorkdayByDefault() { }
    public WorkdayByDefault(int id, int masterId, TimeOnly timeStart, TimeOnly timeEnd, TimeOnly? breakStart, TimeOnly? breakEnd)
    {
        Id = id;
        MasterId = masterId;
        TimeStart = timeStart;
        TimeEnd = timeEnd;
        BreakStart = breakStart;
        BreakEnd = breakEnd;
    }

    public int Id { get; set; }

    public int MasterId { get; set; }

    public TimeOnly TimeStart { get; set; }

    public TimeOnly TimeEnd { get; set; }

    public TimeOnly? BreakStart { get; set; }

    public TimeOnly? BreakEnd { get; set; }

    public virtual Master Master { get; set; } = null!;
}
