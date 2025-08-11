using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("SqlPassword", "Password12*", true);

var mssqlDb = builder.AddSqlServer("SqlServer")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHostPort(1233)
    .WithPassword(password)
    ;

var productDb = mssqlDb.AddDatabase("ProductDb");

builder.AddProject<Aspire_ProductWebAPI>("product")
    .WithReference(productDb)
    .WaitFor(productDb);

builder.AddProject<Aspire_CategoryWebAPI>("category");

builder.Build().Run();
