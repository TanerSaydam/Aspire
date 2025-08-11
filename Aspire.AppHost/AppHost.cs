using Projects;

var builder = DistributedApplication.CreateBuilder(args);

#region Variables
var userName = builder.AddParameter("SqlUserName", "postgres", true);
var password = builder.AddParameter("SqlPassword", "Password12*", true);
#endregion

#region Product
var mssqlDb = builder.AddSqlServer("SqlServer")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHostPort(1233)
    .WithPassword(password)
    ;

var productDb = mssqlDb.AddDatabase("ProductDb");

builder.AddProject<Aspire_ProductWebAPI>("product")
    .WithReference(productDb)
    .WaitFor(productDb);
#endregion

#region Category
var postgreDb = builder.AddPostgres("Postgres", userName, password, 5432)
    .WithLifetime(ContainerLifetime.Persistent);
var categoryDb = postgreDb.AddDatabase("categorydb");

builder.AddProject<Aspire_CategoryWebAPI>("category")
    .WithReference(categoryDb)
    .WaitFor(categoryDb);
#endregion


builder.Build().Run();
