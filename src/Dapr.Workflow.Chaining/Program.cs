using Dapr.Workflow;
using Dapr.Workflow.Chaining.Activities;
using Dapr.Workflow.Chaining.Workflow;
using System.Runtime.Intrinsics.X86;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDaprClient();

builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterWorkflow<DemoWorkflow>();
    options.RegisterActivity<Step1>();
    options.RegisterActivity<Step2>();
    options.RegisterActivity<Step3>();
});


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
