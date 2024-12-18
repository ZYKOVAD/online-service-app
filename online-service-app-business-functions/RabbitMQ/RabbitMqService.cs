using Microsoft.EntityFrameworkCore.Metadata;
using online_service_app_business_functions.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using IModel = RabbitMQ.Client.IModel;

namespace online_service_app_business_functions.RabbitMQ
{
    public class RabbitMqService
    {
        private IConnection _connection;
        private IModel _channel;
        public RabbitMqService()
        {
            var factory = new ConnectionFactory { HostName = "172.17.0.4", Port = 5672};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "event", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMessage(RabbitMqModelMessage mess)
        {
            var message = JsonSerializer.Serialize(mess);

            _channel.QueueDeclare(
                queue: "event",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: "",
                routingKey: "event",
                body: body);
        }
    }
}
