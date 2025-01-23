using MassTransit;
using Messages.EventMessages;

Console.WriteLine("Starting Producer...");

// MassTransit ile RabbitMQ bağlantısını yapılandır
var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("rabbitmq://localhost", h =>
    {
        h.Username("guest");
        h.Password("guest");
    });
});

// RabbitMQ bağlantısını başlat
await busControl.StartAsync();
Console.WriteLine("Producer connected to RabbitMQ.");


try
{
    while (true)
    {
        Console.Write("Enter a message to send (or type 'exit' to quit): ");
        var input = Console.ReadLine();

        if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            break;

        var message = new SampleMessage(Guid.NewGuid(), DateTime.UtcNow, input);

        // Mesajı RabbitMQ'ya gönder
        await busControl.Publish(message);
        Console.WriteLine($"Message sent: {message.Text} at {message.SentAt}");
    }
}
finally
{
    // RabbitMQ bağlantısını durdur
    await busControl.StopAsync();
    Console.WriteLine("Producer stopped.");
}