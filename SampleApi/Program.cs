using hoh.architecture.CQRS.Query;
using hoh.architecture.scaffolding.Extensions;
using SampleApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHohArchitecture();

builder.Services.AddTransient<IQueryHandler<TestQuery, bool>, TestQueryHandler>();

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
