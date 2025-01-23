using MassTransit;
using Messages.EventMessages;

Console.WriteLine("Starting Consumer...");

// MassTransit ile RabbitMQ bağlantısını yapılandır
var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("rabbitmq://localhost", h =>
    {
        h.Username("guest");
        h.Password("guest");
    });

    // Tüketici için bir kuyruk ayarla
    cfg.ReceiveEndpoint("sample-queue", e =>
    {
        e.Consumer<SampleMessageConsumer>();
    });
});

// RabbitMQ bağlantısını başlat
await busControl.StartAsync();
Console.WriteLine("Consumer is listening to messages from RabbitMQ...");

try
{
    Console.ReadLine(); // Çıkış için Enter tuşuna basılmasını bekler
}
finally
{
    // RabbitMQ bağlantısını durdur
    await busControl.StopAsync();
    Console.WriteLine("Consumer stopped.");
}

public class SampleMessageConsumer : IConsumer<SampleMessage>
{
    public Task Consume(ConsumeContext<SampleMessage> context)
    {
        Console.WriteLine($"Message received: {context.Message.Text} at {context.Message.SentAt}");
        return Task.CompletedTask;
    }
}