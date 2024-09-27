using DemoApi.Postgresql.Features.Services;
using DemoApi.PostgreSQL.Features;
using DemoApi.PostgreSQL.Infrastructure.Persistence;
using DemoApi.ServiceDefaults;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleFlag;
using SimpleFlag.AspNetCore;
using SimpleFlag.PostgreSQL;
using System.Security.Claims;
using System.Text.Encodings.Web;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// add the pg database context
builder.Services.AddDbContext<DemoApiDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
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
        customOptions.Migrator = MyDataSourceDatabaseMigration.Instance;
        customOptions.Repository = MyDataSourceRepository.Instance;
    });*/
    options.UsePostgreSQL(pgOptions =>
    {
        pgOptions.SchemaName = "flag";
        pgOptions.TablePrefix = "sf";
        pgOptions.ConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");
    });
});

// Add mock authentication
builder.Services.AddAuthentication("mock")
    .AddScheme<AuthenticationSchemeOptions, MockAuthenticationHandler>("mock", options => { });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SimpleFlagPolicy", policy => policy.RequireClaim("SimpleFlagAdmin"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add simple flag endpoints options
builder.Services.AddEndpointsSimpleFlag(options =>
{
    options.EndpointPrefix = "simpleflag";
    options.ShowApiExplorer = true;
    options.PolicyName = "SimpleFlagPolicy";
});

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

TodoFeature.MapEndpoints(app);

// Add simple flag endpoints
app.MapSimpleFlagEndpoints();

app.Run();


public class MockAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public MockAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] { new Claim("SimpleFlagAdmin", "true"), new Claim(ClaimTypes.Name, "SimpleFlagTestUser") };
        var identity = new ClaimsIdentity(claims, "mock");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "mock");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}