using CleanBlog.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CleanBlog.Infrastructure.Bus
{
    internal class RabbitMQBus : IBus
    {
        private readonly IMediator mediator;
        private readonly ILogger<RabbitMQBus> logger;
        private readonly ConnectionFactory connectionFactory;

        public RabbitMQBus(IMediator mediator,
            ILogger<RabbitMQBus> logger)
        {
            this.mediator = mediator;
            this.logger = logger;

            this.connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IBusEvent
        {
            using var connection = connectionFactory.CreateConnection();

            using var channel = connection.CreateModel();

            var eventName = @event.GetType().Name;

            channel.QueueDeclare(
                        queue: eventName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

            channel.BasicPublish(
                exchange: "",
                routingKey: eventName,
                basicProperties: null,
                body: body
            );
        }

        public void Subscribe<TEvent>() where TEvent : class
        {
            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();

            var eventType = typeof(TEvent);

            channel.QueueDeclare(
                queue: eventType.Name,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var @event = JsonConvert.DeserializeObject(message, eventType) as IBusEvent;

                    mediator.Publish(@event);
                }
                catch (Exception exc)
                {
                    logger.LogError(exc, exc.Message);
                    throw;
                }
            };

            channel.BasicConsume(queue: eventType.Name, autoAck: true, consumer: consumer);
        }
    }
}
