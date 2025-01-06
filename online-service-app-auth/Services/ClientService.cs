using online_service_app_auth.db_layer;
using online_service_app_auth.models;
using online_service_app_auth.repositories;
using System.Collections.Generic;

namespace online_service_app_auth.Services
{
    public class ClientService
    {
        private readonly ClientRepository _repository;
        public ClientService(ClientRepository repository)
        {
            _repository = repository;
        }

        public Client Get(int id)
        {
            Client client = _repository.Get(id);
            return client;
        }

        public Client GetByEmail(string email)
        {
            Client client = _repository.GetByEmail(email);
            return client;
        }

        public Client Register(string name, string surname, string? patronymic, string? phone, string email, string password)
        {
            Client client = _repository.Create(name, surname, patronymic, phone, email, password);
            return client;
        }

    }
}
