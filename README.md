# ROADMAP

 - Query Executor. 
     - logging
     - execution time
     - save query and time 
 - add logging
 - metadata logging. Allows systems to add data, such as user and place of caller







# hoh-Architecture

This project is designed to provide a collection of nuget packages to streamline project development using CQRS, EF Core and some DDD principles.

The heart of the workflow is [CQRS](https://martinfowler.com/bliki/CQRS.html). Each action being its own command, the logic is in one place, it is seperate from other logic, so you do not end up with large service classes.

All commands and queries can be automatically logged for future investigations.

## Steps to setup an application / Quick start

- add nuget package HOH.Architecture to the application
- In the setup add
	- `builder.Services.AddHohArchitecture();`
	- `app.UseHohArchitecture();`
	- Setup any configuration overrides, such as getting from config file (this must happen after `UseHohArchitecture`)
- register the query and command handlers with the IServiceCollection

## Configuration
There are various options that can be set at the top level via configuration.  These can be overridden in a variety of ways. The one variable required if the dfault EF logging is to be used, is to set the connection string.

```
builder.Services.AddHohArchitecture(x =>
{
    x.ConnectionString = "con1";
});
```
Or using appsettings.json
```
builder.Services.Configure<HohArchitectureOptions>(builder.Configuration.GetSection("RootConfig"));
```
Or you could provide [another configuration provider](SampleApi/CustomConfigurationProvider/InMemoryTestCustomConfigurationProvider.cs). These all chain together.  Be sure to call `AddHohArchitecture` first then provide any of your override methods. [see Program.cs](SampleApi/Program.cs) for examples.

The order in which configuration is applied is:

1) builder.Services.AddHohArchitecture => action parameters
2) bindings, such as:
    a) Get section `builder.Services.Configure<HohArchitectureOptions>(builder.Configuration.GetSection("RootConfig"));`
    b) IConfigurationSource `builder.Configuration.Sources.Add(new InMemoryTestCustomConfigurationSource());`

[CommandQueryLoggingType.cs](framework/hoh.architecture.scaffolding/Configuration/HohArchitectureOptions.cs)

See SampleAPI => QueryController for an example of executing a query.  It follows this convention
1. use IServiceProvider to get an instance of the IQueryExecutor.
2. Create an instance of the desired query you wish to run
3. call ExecuteAsync on your IQueryExecutor instance.
4. the handler for that query will be called, perform your business logic here.
5. handle result.

## Creating instances of Command and Query Handlers

There are various ways to instantiate an instance:

 1) Manually register with the IServiceProvider:

 `builder.Services.AddScoped<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();`

Inject the IServiceProvider into the Controller and resolve the handler:

```
public class QueryController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    
    public QueryController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpGet]
    [Route("GetTestQuery")]
    public async Task<IQueryResult<TestQueryResult>> GetTestQuery()
    {
        var query = new TestQuery("This is my message2");
    
        var queryHandler = _serviceProvider.GetService<IQueryHandler<TestQuery, TestQueryResult>>();
    
        var queryResult = await queryHandler.ExecuteAsync(query);
    
        return queryResult;
    }
}
```


 2) Resolve with the IServiceProvider:

 `builder.Services.AddScoped<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();`

Inject the IQueryHandler into the Controller:

```
public class QueryController : ControllerBase
{
    private readonly IQueryHandler<TestQuery, TestQueryResult> _queryHandler;
    
    public QueryController(IQueryHandler<TestQuery, TestQueryResult> queryHandler)
    {
            _queryHandler = queryHandler;
    }

    [HttpGet]
    [Route("GetTestQuery")]
    public async Task<IQueryResult<TestQueryResult>> GetTestQuery()
    {
        var query = new TestQuery("This is my message2");
        var queryResult = await _queryHandler.ExecuteAsync(query);
        return queryResult;
    }
}
```

3) Register ServiceProviderQueryCommandLocator, which will intern use IServiceProvider / setting UseServiceCollection option to true will register `ServiceProviderQueryCommandLocator` and `QueryCommandExecutor`

```
builder.Services.AddScoped<IQueryCommandLocator, ServiceProviderQueryCommandLocator>();
builder.Services.AddScoped<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();
```

Inject the locator and resolve:

```
    public class QueryController : ControllerBase
    {
        private readonly IQueryCommandLocator _locator;

        public QueryController(IQueryCommandLocator locator)
        {
            _locator = locator;
        }

        [HttpGet]
        [Route("GetTestQueryInjected")]
        public async Task<IQueryResult<TestQueryResult>> GetInjectedTestQuery()
        {
            var query = new TestQuery("This is my message3");

            var queryHandler = await _locator.LocateQueryHandlerAsync<TestQuery, TestQueryResult>();
            var queryResult = await queryHandler.ExecuteAsync(query);

            return queryResult;
        }
    }
}
```

4) Implement your own IQueryCommandLocator and inject as above. This can allow abstraction on resolving handlers, as well as using a different DI framework. Custom registration should happen before calling IApplicationBuilder.`AddHohArchitecture`, at least for `IQueryCommandLogging` if you want registration to happen automatically.


## Command and Query logging and statistics

If you simply want to resolve and execute a query or command, the above section will cover that. If you want to have a log:

 - What commands and querys have been executed
 - When they executed
 - How long it took to run

 You will need to use the IQueryCommandExcutor, as well as either use `ServiceProviderQueryCommandLocator` or implement your own version of `IQueryCommandLocator`.

 ```
 using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.QueryCommandHandling;
using hoh.architecture.CQRS.Shared.Results;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Queries;

namespace SampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly IQueryExecutor _executor;

        public QueryController(IQueryExecutor executor)
        {
            _executor = executor;
        }

        [HttpGet]
        [Route("GetTestQueryExecutor")]
        public async Task<IQueryResult<TestQueryResult>> GetExecutorTestQuery()
        {
            var query = new TestQuery("This is my message4");
            return await _executor.ExecuteAsync<TestQuery, TestQueryResult>(query);
        }
    }
}
 ```

 ## Using a different DI framework (not IServiceCollection)

 - set AddHohArchitecture => options => UseServiceCollection to false
 - Implement your own IQueryCommandLocator
 - Add QueryCommandExecutor (IQueryCommandExecutor) to your DI.
 - Register Command and Query handlers in your DI.

## Using IServiceCollection

 - set AddHohArchitecture => options => UseServiceCollection to true
 - Register QueryHandlers (example assumes all query handlers are in the same assembly) => builder.Services.RegisterQueryHandlers(ServiceLifetime.Scoped, typeof(ExampleQueryHandler).Assembly);
 - Register CommandHandlers (example assumes all command handlers are in the same assembly) => builder.Services.RegisterCommandHandlers(ServiceLifetime.Scoped, typeof(ExampleQueryHandler).Assembly);


 ## Registering DB logging 

 - If you do not want to use logging, not not register any `ICommandQueryLogging`

 - `services.RegisterCommandQueryLogging()` will register the default `EntityFrameworkCommandQueryLogger`
 - `services.RegisterCommandQueryLogging<T>()` will register T your own implementation of `ICommandQueryLogging`


 - You can manually builder.Services.AddDbContext<YourCommandQueryLogger, YouLoggingDbContext>(options => {}) during service registration
 - Use AddHohArchitecture overload. 
 ```
builder.Services.AddHohArchitecture<EntityFrameworkCommandQueryLogger, LoggingDbContext>(x =>
{
    x.ConnectionString = "con1";
    x.TableName = "LoggingQueryCommands";
    x.UseServiceCollection = true;
});
 ```