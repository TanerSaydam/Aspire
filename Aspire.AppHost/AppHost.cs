using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Aspire_ProductWebAPI>("product");
builder.AddProject<Aspire_CategoryWebAPI>("category");

builder.Build().Run();
