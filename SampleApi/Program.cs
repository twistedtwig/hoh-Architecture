using hoh.architecture.CQRS.Query;
using hoh.architecture.scaffolding.Configuration;
using hoh.architecture.scaffolding.Extensions;
using SampleApi.CustomConfigurationProvider;
using SampleApi.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddHohArchitecture();
builder.Services.AddHohArchitecture(x =>
{
    x.CommandLogging.CommandLoggingConnectionString = "con1";
    x.QueryLogging.QueryLoggingConnectionString = "con2";
});

builder.Services.Configure<HohArchitectureOptions>(builder.Configuration.GetSection("RootConfig"));
builder.Configuration.Sources.Add(new InMemoryTestCustomConfigurationSource());

builder.Services.AddTransient<IQueryHandler<TestQuery, string>, TestQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHohArchitecture();

app.MapControllers();

app.Run();
