using Aspire.Hosting.Dapr;
using System.Collections.Immutable;

var builder = DistributedApplication.CreateBuilder(args);

/*
var secrets = builder.ExecutionContext.IsPublishMode
    ? builder.AddAzureKeyVault("secrets")
    : builder.AddConnectionString("secrets");
*/
var secrets = builder.AddConnectionString("secrets");

builder.AddProject<Projects.Dapr_Workflow_AsyncApi>("dapr-workflow-asyncapi")
    .WithDaprSidecar();

builder.AddProject<Projects.eShopLite_Workflows>("eshoplite-workflows")
    .WithDaprSidecar();

var api = builder.AddProject<Projects.eShopLite_Api>("eshoplite-api")
    .WithReference(secrets);

builder.AddProject<Projects.Store>("store")
    .WithExternalHttpEndpoints()
    .WithReference(api);  // store project depends on products project

builder.Build().Run();
