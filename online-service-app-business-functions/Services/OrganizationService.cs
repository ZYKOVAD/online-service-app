﻿using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Repositories;

namespace online_service_app_business_functions.Services
{
    public class OrganizationService
    {
        private readonly OrganizationRepository _organizationRepository;
        public OrganizationService(OrganizationRepository repos)
        {
            _organizationRepository = repos;
        } 

        public Organization Get(int id)
        {
            return _organizationRepository.Get(id);
        }

        public List<Organization> GetAll()
        {
            return _organizationRepository.GetAll();
        }

        public List<int> GetOrganizationIdsByClient(int clientId)
        {
            return _organizationRepository.GetOrganizationIdsByClient(clientId);
        }

        public Organization Update(int id, OrganizationModel model)
        {
            return _organizationRepository.Update(id, model);
        }

        public bool Delete(int id)
        {
            return _organizationRepository.Delete(id);
        }
    }
}
