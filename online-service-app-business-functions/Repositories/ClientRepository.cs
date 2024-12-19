using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;

namespace online_service_app_business_functions.Repositories
{
    public class ClientRepository
    {
        private readonly OnlineServiceDbContext _db;
        public ClientRepository(OnlineServiceDbContext db)
        {
            _db = db;
        }

        public Client Get(int id)
        {
            Client client = _db.Clients.SingleOrDefault(o => o.Id == id);
            if (client == null) throw new Exception("Клиент с таким id не найден");
            else return client;
        }

        public Client Update(int  id, ClientModel model)
        {
            Client client = _db.Clients.SingleOrDefault(o => o.Id == id);
            if (client == null) throw new Exception("Клиент с таким id не найден");
            else
            {
                client.Name = model.Name;
                client.Surname = model.Surname;
                client.Patronymic = model.Patronymic;
                client.Phone = model.Phone;
                client.Email = model.Email;
                _db.SaveChanges();
                return client;
            }
        }

        public bool Delete(int id)
        {
            Client client = _db.Clients.SingleOrDefault(o => o.Id == id);
            if (client == null) throw new Exception("Клиент с таким id не найден");
            else
            {
                _db.Clients.Remove(client);
                _db.SaveChanges();
                return true;
            }
        }
    }
}
