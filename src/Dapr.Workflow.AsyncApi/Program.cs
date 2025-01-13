using Dapr.Workflow;
using Dapr.Workflow.AsyncApi.Activities;
using Dapr.Workflow.AsyncApi.Models;
using Dapr.Workflow.AsyncApi.Workflows;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterWorkflow<DemoWorkflow>();
    options.RegisterActivity<ProcessPaymentActivity>();
    options.RegisterActivity<NotifyWarehouseActivity>();
});

//https://github.com/dapr/dotnet-sdk/tree/master/examples/Workflow/WorkflowAsyncOperations

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();

await using var scope = app.Services.CreateAsyncScope();
var daprWorkflowClient = scope.ServiceProvider.GetRequiredService<DaprWorkflowClient>();

var instanceId = $"demo-workflow-{Guid.NewGuid().ToString()[..8]}";
var transaction = new Transaction(16.58m);
await daprWorkflowClient.ScheduleNewWorkflowAsync(nameof(DemoWorkflow), instanceId, transaction);

//Poll for status updates every second
var status = await daprWorkflowClient.GetWorkflowStateAsync(instanceId);
do
{
    Console.WriteLine($"Current status: {status.RuntimeStatus}, step: {status.ReadCustomStatusAs<string>()}");
    status = await daprWorkflowClient.GetWorkflowStateAsync(instanceId);
} while (!status.IsWorkflowCompleted);

Console.WriteLine($"Workflow completed - {status.ReadCustomStatusAs<string>()}");
