using Dapr.Workflow;
using eShopLite.Workflows.Activities;
using eShopLite.Workflows.OrderProcessor;
using Google.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();


builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterWorkflow<OrderProcessingWorkflow>();

    // These are the activities that get invoked by the workflow(s).
    options.RegisterActivity<NotifyActivity>();
    options.RegisterActivity<ReserveInventoryActivity>();
    options.RegisterActivity<ProcessPaymentActivity>();
    options.RegisterActivity<UpdateInventoryActivity>();
});


// Add services to the container.
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

# region Request Pipeline

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

#endregion