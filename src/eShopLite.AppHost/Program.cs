using Aspire.Hosting.Dapr;
using System.Collections.Immutable;

var builder = DistributedApplication.CreateBuilder(args);

/*
var secrets = builder.ExecutionContext.IsPublishMode
    ? builder.AddAzureKeyVault("secrets")
    : builder.AddConnectionString("secrets");
*/
var secrets = builder.AddConnectionString("secrets");

var products = builder.AddProject<Projects.Products>("products").WithReference(secrets); //products name will be used to discover the address of the Products project.

builder.AddProject<Projects.Store>("store")
    .WithExternalHttpEndpoints()
    .WithReference(products);  // store project depends on products project

builder.AddProject<Projects.eShopLite_Api>("eshoplite-api")
    .WithReference(secrets);
    

builder.AddProject<Projects.eShopLite_Worker>("eshoplite-worker");


builder.AddProject<Projects.eShop_Workflow_TaskChaining>("eshop-workflow-taskchaining");

builder.Build().Run();
