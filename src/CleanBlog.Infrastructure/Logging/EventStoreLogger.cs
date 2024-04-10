using CleanBlog.Domain.Abstractions;
using EventStore.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace CleanBlog.Infrastructure.Logging.EventSourcing
{
    internal class EventStoreLogger : IEventSourcing
    {
        private readonly EventStoreClient client;
        private const string connectionString = "esdb://admin:changeit@localhost:2113?tls=false&tlsVerifyCert=false";

        private readonly ILogger<EventStoreLogger> logger;

        public EventStoreLogger(ILogger<EventStoreLogger> logger)
        {
            this.logger = logger;
            var settings = EventStoreClientSettings.Create(connectionString);
            client = new EventStoreClient(settings);
        }

        public async Task AppendAsync(string type, object data, CancellationToken ct)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            var eventData = new EventData(Uuid.NewUuid(), type, Encoding.UTF8.GetBytes(jsonData));

            try
            {
                await client.AppendToStreamAsync(nameof(CleanBlog), StreamState.Any, new[] { eventData }, cancellationToken: ct);
            }
            catch (Grpc.Core.RpcException exc)
            {
                logger.LogError(exc, exc.Message);
            }
        }

        // This is for reading/getting logs from event store db.
        //public async Task ReadAsync(CancellationToken cancellationToken)
        //{
        //    string connectionString = "esdb://admin:changeit@localhost:2113?tls=false&tlsVerifyCert=false";

        //    var settings = EventStoreClientSettings.Create(connectionString);
        //    var client = new EventStoreClient(settings);

        //    var result = client.ReadStreamAsync(Direction.Forwards, "insurance-stream", StreamPosition.Start, cancellationToken: cancellationToken);
        //    var events = await result.Where(c => c.Event.EventType == eventType).ToListAsync(cancellationToken);
        //    List<Category> list = new List<Category>();
        //    foreach (var @event in events)
        //    {
        //        list.Add(JsonConvert.DeserializeObject<Category>(Encoding.UTF8.GetString(@event.Event.Data.ToArray())));
        //    }
        //    return list;
        //}
    }
}
