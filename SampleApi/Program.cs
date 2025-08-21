using hoh.architecture.scaffolding.Configuration;
using hoh.architecture.scaffolding.Extensions;
using SampleApi.CustomConfigurationProvider;

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
    x.UseServiceCollection = true;
});

builder.Services.Configure<HohArchitectureOptions>(builder.Configuration.GetSection("RootConfig"));
builder.Configuration.Sources.Add(new InMemoryTestCustomConfigurationSource());

builder.Services.RegisterQueryHandlers(ServiceLifetime.Scoped, typeof(Program).Assembly);
builder.Services.RegisterCommandHandlers(ServiceLifetime.Scoped, typeof(Program).Assembly);

Console.WriteLine("test test test test");

////TODO this should be in the AddHohArchitecture setup process.
//builder.Services.AddScoped<IQueryCommandLocator, ServiceProviderQueryCommandLocator>();

////TODO this should be in the AddHohArchitecture setup process.
//builder.Services.AddScoped<IQueryExecutor, QueryExecutor>();

//builder.Services.RegisterQueryHandlers(ServiceLifetime.Scoped, typeof(Program).Assembly);

//builder.Services.AddScoped<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();
//builder.Services.AddScoped<IQueryHandler<TestMathQuery, TestMathQueryResult>, TestMathQueryHandler>();

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
