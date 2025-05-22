using Confluent.Kafka;

namespace RealEstate.Properties.Infrastructure.Kafka.Consumer
{
    public class PropertyEventConsumer
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly string _topic;

        public PropertyEventConsumer(string bootstrapServers, string topic, string groupId)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _topic = topic;
        }

        public void StartConsuming(Action<string> handleMessage, CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(cancellationToken);
                    handleMessage(consumeResult.Message.Value);
                }
            }, cancellationToken);
        }
    }
}
