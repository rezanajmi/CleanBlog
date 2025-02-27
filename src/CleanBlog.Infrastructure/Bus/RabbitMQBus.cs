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

        public async Task Publish<TEvent>(TEvent @event, CancellationToken ct = default) where TEvent : IBusEvent
        {
            using var connection = await connectionFactory.CreateConnectionAsync(ct);

            using var channel = await connection.CreateChannelAsync(cancellationToken: ct);

            var eventName = @event.GetType().Name;

            await channel.QueueDeclareAsync(
                        queue: eventName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null,
                        noWait: false,
                        cancellationToken: ct
                    );

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: eventName,
                body: body,
                cancellationToken: ct
                );
        }

        public async Task Subscribe<TEvent>(CancellationToken ct = default) where TEvent : class
        {
            var connection = await connectionFactory.CreateConnectionAsync(ct);

            var channel = await connection.CreateChannelAsync(cancellationToken: ct);

            var eventType = typeof(TEvent);

            await channel.QueueDeclareAsync(
                queue: eventType.Name,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                noWait: false,
                cancellationToken: ct
                );

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var @event = JsonConvert.DeserializeObject(message, eventType) as IBusEvent;

                    await mediator.Publish(@event, ct);
                }
                catch (Exception exc)
                {
                    logger.LogError(exc, exc.Message);
                    throw;
                }
            };

            await channel.BasicConsumeAsync(
                queue: eventType.Name,
                autoAck: true,
                consumer: consumer,
                cancellationToken: ct
                );
        }
    }
}
