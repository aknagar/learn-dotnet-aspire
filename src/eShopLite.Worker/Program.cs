using eShopLite.Worker;
using Microsoft.Extensions.Azure;

var builder = Host.CreateApplicationBuilder(args);

builder.AddAzureServiceBusClient("serviceBus");

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
