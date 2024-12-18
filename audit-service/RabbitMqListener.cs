using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace audit_service
{
    public class RabbitMqListener : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        public RabbitMqListener()
        {
            var factory = new ConnectionFactory { HostName = "172.17.0.4", Port = 5672 };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "event", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                RabbitMqModelMesssage message = JsonConvert.DeserializeObject<RabbitMqModelMesssage>(content);

                Db.Data.Add(message);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("event", false, consumer);

            return Task.CompletedTask;
        }
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
