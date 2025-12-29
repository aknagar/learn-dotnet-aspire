# OpenAPI Documentation NuGet Packages

This document lists the NuGet packages used for OpenAPI documentation in this project.

## Packages Used

### 1. Microsoft.AspNetCore.OpenApi
- **Version**: 9.0.0
- **Purpose**: Provides OpenAPI document generation for ASP.NET Core applications
- **Used in**: 
  - `eShopLite.Api` project
  - `Dapr.Workflow.AsyncApi` project

This is the official Microsoft package for OpenAPI support in ASP.NET Core. It's the next generation of Swagger/OpenAPI integration for .NET applications.

### 2. Scalar.AspNetCore
- **Version**: 1.2.75
- **Purpose**: Provides a modern, interactive API documentation UI (similar to Swagger UI)
- **Used in**: `eShopLite.Api` project

Scalar provides a beautiful, modern alternative to Swagger UI for viewing and interacting with your OpenAPI documentation.

## Implementation Details

### eShopLite.Api Project

In the `eShopLite.Api/eShopLite.Api.csproj` file:

```xml
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.0" />
<PackageReference Include="Scalar.AspNetCore" Version="1.2.75" />
```

### Usage in Program.cs

The OpenAPI functionality is configured in `Program.cs`:

```csharp
// Add OpenAPI services
builder.Services.AddOpenApi();  // OpenAPI is the next version swagger

// Configure middleware (in development environment)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); //publish endpoint at /openapi/v1.json
    app.MapScalarApiReference(); // similar to swagger UI at /scalar/v1
}
```

## Accessing the Documentation

When running the application in development mode:

- **OpenAPI JSON**: Available at `/openapi/v1.json`
- **Scalar UI**: Available at `/scalar/v1`

## Why These Packages?

- **Microsoft.AspNetCore.OpenApi**: This is the official, built-in OpenAPI support for .NET 9+, replacing the older Swashbuckle.AspNetCore package. It provides better performance and tighter integration with ASP.NET Core.

- **Scalar.AspNetCore**: Provides a modern, clean UI for API documentation that's more user-friendly than traditional Swagger UI, with better performance and modern design.
