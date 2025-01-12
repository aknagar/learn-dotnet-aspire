var builder = DistributedApplication.CreateBuilder(args);

var products = builder.AddProject<Projects.Products>("products"); //products name will be used to discover the address of the Products project.

builder.AddProject<Projects.Store>("store")
    .WithExternalHttpEndpoints()
    .WithReference(products);  // store project depends on products project


builder.AddProject<Projects.eShopLite_Api>("eshoplite-api");


builder.AddProject<Projects.eShopLite_Worker>("eshoplite-worker");


builder.Build().Run();
