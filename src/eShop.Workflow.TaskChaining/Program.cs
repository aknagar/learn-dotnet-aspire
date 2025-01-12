using Dapr.Workflow;
using eShop.Workflow.TaskChaining;
using eShop.Workflow.TaskChaining.Activities;
using eShop.Workflow.TaskChaining.Workflow;

//https://github.com/dapr/dotnet-sdk/blob/master/examples/Workflow/WorkflowTaskChaining/Program.cs
var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDaprWorkflow(options =>
    {
        options.RegisterWorkflow<DemoWorkflow>();
        options.RegisterActivity<Step1>();
        options.RegisterActivity<Step2>();
        options.RegisterActivity<Step3>();
    });
    
});

// Start the app - this is the point where we connect to the Dapr sidecar to listen 
// for workflow work-items to execute
var host = builder.Build();
host.StartAsync();

await using var scope = host.Services.CreateAsyncScope();
var daprWorkflowClient = scope.ServiceProvider.GetRequiredService<DaprWorkflowClient>();

//Check health
const int wfInput = 42;
Console.WriteLine(@"Workflow Started");

var instanceId = $"demo-workflow-{Guid.NewGuid().ToString()[..8]}";

//Start the workflow immediately
await daprWorkflowClient.ScheduleNewWorkflowAsync(nameof(DemoWorkflow), instanceId, wfInput);

//Get the status of the workflow
WorkflowState workflowState;
while (true)
{
    workflowState = await daprWorkflowClient.GetWorkflowStateAsync(instanceId, true);
    Console.WriteLine($@"Workflow status: {workflowState.RuntimeStatus}");
    if (workflowState.IsWorkflowCompleted)
        break;

    await Task.Delay(TimeSpan.FromSeconds(1));
}

//Display the result from the workflow
var result = string.Join(" ", workflowState.ReadOutputAs<int[]>() ?? Array.Empty<int>());
Console.WriteLine($@"Workflow result: {result}");