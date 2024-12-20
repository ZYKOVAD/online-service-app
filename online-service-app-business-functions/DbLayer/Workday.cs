using System;
using System.Collections.Generic;

namespace online_service_app_business_functions.DbLayer;

public partial class Workday
{
    public Workday() { }
    public Workday(int id, int masterId, DateOnly date, TimeOnly timeStart, TimeOnly timeEnd, TimeOnly? breakStart, TimeOnly? breakEnd)
    {
        Id = id; 
        MasterId = masterId; 
        Date = date; 
        TimeStart = timeStart; 
        TimeEnd = timeEnd; 
        BreakStart = breakStart;
        BreakEnd = breakEnd;
    }
    public int Id { get; set; }

    public int MasterId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly TimeStart { get; set; }

    public TimeOnly TimeEnd { get; set; }

    public TimeOnly? BreakStart { get; set; }

    public TimeOnly? BreakEnd { get; set; }

    public virtual Master Master { get; set; } = null!;
}
