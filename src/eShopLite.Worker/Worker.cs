using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;

namespace eShopLite.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ServiceBusClient _client;

    public Worker(ILogger<Worker> logger, ServiceBusClient client)
    {
        _logger = logger;
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            var processor = _client.CreateProcessor(
                "notifications",
                "mobile",
                new ServiceBusProcessorOptions());

            // Add handler to process messages
            processor.ProcessMessageAsync += MessageHandler;

            // Add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;

            // Start processing
            await processor.StartProcessingAsync();

            _logger.LogInformation("""
                Wait for a minute and then press any key to end the processing
                """);

            Console.ReadKey();

            // Stop processing
            _logger.LogInformation("""

                Stopping the receiver...
                """);

            await processor.StopProcessingAsync();

            _logger.LogInformation("Stopped receiving messages");
        }

        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            _logger.LogInformation("Received: {Body} from subscription.", body);

            // Complete the message. messages is deleted from the subscription.
            await args.CompleteMessageAsync(args.Message);
        }

        // Handle any errors when receiving messages
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception, "{Error}", args.Exception.Message);

            return Task.CompletedTask;
        }
    }
}
