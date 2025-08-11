using Projects;

var builder = DistributedApplication.CreateBuilder(args);

#region Variables
var userName = builder.AddParameter("SqlUserName", "postgres", true);
var password = builder.AddParameter("SqlPassword", "Password12*", true);
#endregion

#region Product
//var mssqlDb = builder.AddSqlServer("SqlServer")
//    .WithLifetime(ContainerLifetime.Persistent)
//    .WithHostPort(1233)
//    .WithPassword(password)
//    ;

//var productDb = mssqlDb.AddDatabase("ProductDb");

var rabbitMq = builder.AddRabbitMQ("rabbitMQ", null, null, 5672)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithImage("rabbitmq", "3-management")
    .WithHttpEndpoint(port: 15672, targetPort: 15672, name: "management")
    .WithEnvironment("RABBITMQ_DEFAULT_USER", "guest")
    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "guest")
    ;

var redis = builder.AddRedis("cache")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHostPort(6379);

builder.AddProject<Aspire_ProductWebAPI>("product")
    //.WithReference(productDb)
    //.WaitFor(productDb)
    .WithReference(redis)
    .WaitFor(redis);
#endregion

#region Category
//var postgreDb = builder.AddPostgres("Postgres", userName, password, 5432)
//    .WithLifetime(ContainerLifetime.Persistent)
//    ;
//var categoryDb = postgreDb.AddDatabase("categorydb");

//builder.AddProject<Aspire_CategoryWebAPI>("category")
//    .WithReference(categoryDb)
//    .WaitFor(categoryDb);
#endregion


builder.Build().Run();
