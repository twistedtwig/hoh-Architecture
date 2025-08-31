using hoh.architecture.CQRS.Logging;
using hoh.architecture.scaffolding.Extensions;
using hoh.architecture.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using SampleApi.CustomConfigurationProvider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.Sources.Add(new InMemoryTestCustomConfigurationSource());
builder.Services.Configure<HohArchitectureOptions>(builder.Configuration.GetSection("RootConfig"));

builder.Services.AddHohArchitecture<LoggingDbContext>(x =>
{
    x.ConnectionString = "con1";
    x.CommandLogging.TableName = "LoggingQueryCommands";
    x.CommandLogging.Type = CommandQueryLoggingType.None;

    x.QueryLogging.TableName = "new table name";
    x.QueryLogging.Type = CommandQueryLoggingType.None;

    x.UseServiceCollection = true;
}, dbOptions =>
{
    dbOptions
        .UseSqlServer("con1")
        .EnableSensitiveDataLogging(true);
});

// Add services to the container.
builder.Services.RegisterQueryHandlers(ServiceLifetime.Scoped, typeof(Program).Assembly);
builder.Services.RegisterCommandHandlers(ServiceLifetime.Scoped, typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        var x = options.OpenApiVersion;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHohArchitecture();

app.MapControllers();

app.Run();
