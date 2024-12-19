﻿namespace online_service_app_business_functions.Models
{
    public class MasterModel
    {
        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string? Patronymic { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public int SpecializationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
