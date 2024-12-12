using System;
using System.Collections.Generic;

namespace online_service_app_business_functions.db_layer;

public partial class Specialization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Master> Masters { get; set; } = new List<Master>();
}
