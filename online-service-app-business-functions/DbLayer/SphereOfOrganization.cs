using System;
using System.Collections.Generic;

namespace online_service_app_business_functions.DbLayer;

public partial class SphereOfOrganization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();
}
