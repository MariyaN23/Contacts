var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.Contacts_API>("contacts-api");

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
