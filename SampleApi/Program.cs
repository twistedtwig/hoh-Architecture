using HoH.Architecture.CQRS.Logging;
using HoH.Architecture.scaffolding.Extensions;
using HoH.Architecture.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using SampleApi.Commands;
using SampleApi.CustomConfigurationProvider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.Sources.Add(new InMemoryTestCustomConfigurationSource());
builder.Services.Configure<HohArchitectureOptions>(builder.Configuration.GetSection("RootConfig"));

builder.Services.AddHohArchitecture<EntityFrameworkCommandQueryLogger, LoggingDbContext>(x =>
{
    x.CommandQueryLoggingConnectionString = @"Data Source=localhost;Initial Catalog=YourDatabase2; uid=sa;pwd=123456;MultipleActiveResultSets=true; TrustServerCertificate=True;Trusted_Connection=True;";

    x.UseServiceCollection = true;
});

// Add services to the container.
builder.Services.RegisterQueryHandlers(ServiceLifetime.Scoped, typeof(Program).Assembly);
builder.Services.RegisterCommandHandlers(ServiceLifetime.Scoped, typeof(Program).Assembly);

builder.Services.AddDbContext<ExampleDbContext>(x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=ExampleDb; uid=sa;pwd=123456;MultipleActiveResultSets=true; TrustServerCertificate=True;Trusted_Connection=True;"));

var app = builder.Build();

app.CreateDatabaseRunMigrations<ExampleDbContext>();

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
