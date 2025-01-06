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
            Client client = _clientRepository.Get(id);
            return client;
        }

        public Client Update(int id, ClientModel model)
        {
            Client client = _clientRepository.Update(id, model);
            return client;
        }

        public bool Delete(int id)
        {
            bool result = _clientRepository.Delete(id);
            return result;
        }
    }
}
