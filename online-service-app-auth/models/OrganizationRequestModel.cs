﻿namespace online_service_app_auth.models
{
    public class OrganizationRequestModel
    {
        public string Name { get; set; } = null!;

        public int TypeId { get; set; }

        public int? SphereId { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? WebAddress { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
