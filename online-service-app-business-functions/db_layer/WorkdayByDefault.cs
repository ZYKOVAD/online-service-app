using System;
using System.Collections.Generic;

namespace online_service_app_business_functions.db_layer;

public partial class WorkdayByDefault
{
    public int Id { get; set; }

    public int MasterId { get; set; }

    public TimeOnly TimeStart { get; set; }

    public TimeOnly TimeEnd { get; set; }

    public TimeOnly? BreakStart { get; set; }

    public TimeOnly? BreakEnd { get; set; }

    public virtual Master Master { get; set; } = null!;
}
