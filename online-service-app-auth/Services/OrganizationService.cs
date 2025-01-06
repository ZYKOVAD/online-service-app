using online_service_app_auth.db_layer;
using online_service_app_auth.Repositories;

namespace online_service_app_auth.Services
{
    public class OrganizationService
    {
        public readonly OrganizationRepository _organizationRepository;
        public OrganizationService(OrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public Organization GetByEmail(string email)
        {
            Organization organization = _organizationRepository.GetByEmail(email);
            return organization;
        }

        public Organization Register(string name, int typeId, int? shereId, string? phone, string? address, string? webAddress, string email, string password)
        {
            Organization organization = _organizationRepository.Create(name, typeId, shereId, phone, address, webAddress, email, password);
            return organization;
        }
    }
}
