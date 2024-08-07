using DemoApi.Postgresql.Features.Services;
using DemoApi.PostgreSQL.Features;
using DemoApi.PostgreSQL.Infrastructure.Persistence;
using DemoApi.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using SimpleFlag;
using SimpleFlag.AspNetCore;
using SimpleFlag.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// add the pg database context
builder.Services.AddDbContext<DemoApiDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});

//add logger 
builder.Services.AddLogging(config =>
{
    config.AddConsole();
});

builder.Services.AddSimpleFlag(options =>
{
    //options.UsePostgreSQL(builder.Configuration.GetConnectionString("PostgresConnection")); // this is the same as the next line

    // using custom data source
    /*options.UseCustom(customOptions =>
    {
        customOptions.SchemaName = "flag";
        customOptions.TablePrefix = "sf";
        customOptions.ConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");
        customOptions.DataSourceMigration = MyDataSourceDatabaseMigration.Instance;
        customOptions.DataSourceRepository = MyDataSourceRepository.Instance;
    });*/
    options.UsePostgreSQL(pgOptions =>
    {
        pgOptions.SchemaName = "flag";
        pgOptions.TablePrefix = "sf";
        pgOptions.ConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");
        pgOptions.Domain = "demo";
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapSimpleFlagEndpoints();

TodoFeature.MapEndpoints(app);

app.Run();
