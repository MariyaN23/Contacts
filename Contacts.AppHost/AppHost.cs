using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataBindMount("./postgres_data");

var db = postgres.AddDatabase("db");

var api = builder.AddProject<Projects.Contacts_API>("contacts-api")
    .WithReference(db);

var client = builder
    .AddViteApp("contacts-client", "../Contacts.Client")
    .WithReference(api)
    .WithEndpoint("http", endpoint =>
    {
        endpoint.Port = 5173;       
    })
    .WithEnvironment("VITE_PORT", "5173")
    .PublishAsDockerFile();

builder.Build().Run();
