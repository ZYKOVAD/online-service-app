using Microsoft.AspNetCore.SignalR;
using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Repositories;

namespace online_service_app_business_functions.Services
{
    public class ClientService
    {
        private readonly ClientRepository _clientRepository;
        public ClientService(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client Get(int id)
        {
            return _clientRepository.Get(id);
        }

        public Client Update(int id, ClientModel model)
        {
            return _clientRepository.Update(id, model);
        }

        public bool Delete(int id)
        {
            return _clientRepository.Delete(id);
        }
    }
}
