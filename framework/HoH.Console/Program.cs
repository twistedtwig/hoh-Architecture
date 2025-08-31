// See https://aka.ms/new-console-template for more information
using HoH.Architecture.CQRS.Logging;
using HoH.Architecture.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");


var services = new ServiceCollection();

services.AddOptions<HohArchitectureOptions>().Configure(x => x.TableName = "TestTableName" );

services
    .AddDbContext<LoggingDbContext>(x =>
    {
        x.UseSqlServer(@"Data Source=localhost;Initial Catalog=frameworkDb; uid=sa;pwd=123456;MultipleActiveResultSets=true; TrustServerCertificate=True;Trusted_Connection=True;");
    });


