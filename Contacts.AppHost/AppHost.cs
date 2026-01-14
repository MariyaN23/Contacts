var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Contacts_API>("contacts-api");

builder.Build().Run();
