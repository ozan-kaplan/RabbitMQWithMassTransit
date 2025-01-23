# RabbitMQ with MassTransit Console Applications

This project demonstrates the use of **MassTransit** with **RabbitMQ** to create two console applications:

1. **Producer**: Sends messages to a RabbitMQ exchange.
2. **Consumer**: Listens to a RabbitMQ queue and processes incoming messages.

---

## Prerequisites

1. **.NET 8 SDK** installed.
2. **RabbitMQ** instance running.
   - You can use Docker to set up RabbitMQ:
     ```bash
     docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
     ```
   - RabbitMQ Management Panel: [http://localhost:15672](http://localhost:15672)  
     Default credentials:
     - Username: `guest`
     - Password: `guest`
3. **NuGet Packages**: Install required dependencies in both the `Producer` and `Consumer` projects:
   ```bash
   dotnet add package MassTransit
   dotnet add package MassTransit.RabbitMQ
   ```

---

## Running the Applications

### Step 1: Start RabbitMQ
Ensure RabbitMQ is running locally or on a configured server. You can verify its status using the RabbitMQ Management Panel.

### Step 2: Run the Producer
The **Producer** application allows you to send messages to RabbitMQ.

Navigate to the Producer project directory and run:
```bash
dotnet run
```
You will be prompted to enter messages. Type a message and press `Enter` to send it. Type `exit` to stop the application.

### Step 3: Run the Consumer
The **Consumer** application listens to a RabbitMQ queue and processes incoming messages.

Navigate to the Consumer project directory and run:
```bash
dotnet run
```
The consumer will display any received messages in the console.

---

## Project Structure

### Producer Application
The producer application is responsible for:
1. Establishing a connection to RabbitMQ using MassTransit.
2. Publishing messages to an exchange with a specified message format.



### Consumer Application
The consumer application is responsible for:
1. Connecting to a RabbitMQ queue.
2. Listening for messages on the queue.
3. Processing messages using a consumer class.

---

## Configuration
Both applications use the default RabbitMQ connection settings:
- Host: `rabbitmq://localhost`
- Username: `guest`
- Password: `guest`

To modify these settings, update the following sections in the `Program.cs` file of both applications:
```csharp
cfg.Host("rabbitmq://localhost", h =>
{
    h.Username("guest");
    h.Password("guest");
});
```

---

## Advanced Features

### Native AOT Compilation
You can compile the applications to native executables for better performance and reduced deployment size:
```bash
dotnet publish -r win-x64 -c Release /p:PublishAot=true
```
This is supported in .NET 8 and provides significant performance improvements.

### Observing Messages in RabbitMQ
1. Open the RabbitMQ Management Panel at [http://localhost:15672](http://localhost:15672).
2. Go to the **Queues** section.
3. Monitor the `sample-queue` to observe incoming messages.

---

## Example Output

### Producer Console Output:
```
Starting Producer...
Producer connected to RabbitMQ.
Enter a message to send (or type 'exit' to quit): Hello, RabbitMQ!
Message sent: Hello, RabbitMQ! at 2025-01-23 10:00:00
```

### Consumer Console Output:
```
Starting Consumer...
Consumer is listening to messages from RabbitMQ...
Message received: Hello, RabbitMQ! at 2025-01-23 10:00:00
```

---

## License
This project is licensed under the MIT License. Feel free to use and modify it as needed.