using Contacts.Application.Services;
using Contacts.Core.Abstractions;
using Contacts.DataAccess;
using Contacts.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContactsDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ContactsDbContext)));
    });

builder.Services.AddScoped<IContactsService, ContactsService>();
builder.Services.AddScoped<IContactsRepository, ContactsRepository>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:5173");
    x.WithMethods().AllowAnyMethod();
});

app.Run();
