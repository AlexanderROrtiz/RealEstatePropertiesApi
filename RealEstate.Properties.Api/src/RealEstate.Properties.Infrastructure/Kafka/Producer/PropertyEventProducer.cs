
using Confluent.Kafka;
using RealEstate.Properties.Application.Interfaces.Kafka;
using RealEstate.Properties.Application.Kafka.Interfaces;
using System.Text.Json;

namespace RealEstate.Properties.Infrastructure.Kafka.Producer
{
    public class PropertyEventProducer: IPropertyEventProducer
    {
        private readonly IProducer<Null, string> _producer;
        private readonly IEventSelector _eventSelector;

        public PropertyEventProducer(string bootstrapServers, IEventSelector eventSelector)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
            _eventSelector = eventSelector;
        }

        public async Task PublishAsync<T>(T @event)
        {
            var topic = _eventSelector.GetTopicForEvent(@event);
            var message = new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(@event)
            };
            await _producer.ProduceAsync(topic, message);
            Console.WriteLine($"Mensaje publicado en el tópico de Kafka: '{topic}': {JsonSerializer.Serialize(@event)}");
        }
    }
}