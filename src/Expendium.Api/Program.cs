using Expendium.Api.Infrastructure;
using Expendium.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ExpendiumDbContext>(c =>
    c.UseNpgsql(builder.Configuration.GetConnectionString("Expendium"), x =>
        x.MigrationsAssembly("Expendium.Data.Postgres"))
);

builder.Services.AddHostedService<MigrationHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints();

await app.RunAsync();