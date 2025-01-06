using online_service_app_auth.db_layer;
using online_service_app_auth.models;
using online_service_app_auth.Repositories;

namespace online_service_app_auth.Services
{
    public class MasterService
    {
        public readonly MasterRepository _masterRepository;

        public MasterService(MasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public Master GetByEmail(string email)
        {
            Master master = _masterRepository.GetByEmail(email);
            return master;
        }
        public Master Register(string name, string surname, string? patronymic, string? phone, string email, string password, int specializationId, int organizationId)
        {
            Master master = _masterRepository.Create(name, surname, patronymic, phone, email, password, specializationId, organizationId);
            return master;
        }
    }
}
