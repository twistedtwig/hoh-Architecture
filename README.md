# ROADMAP

 - Query Executor. 
     - logging
     - execution time
     - save query and time 
 - add ICommandQueryLocator to setup `UseServiceProvider` 
 - add function to setup to locate all commands and queries from assembly(s) with ServiceProvider
 - add logging








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
There are various options that can be set at the top level via configuration, [see CommandQueryLoggingType](framework/hoh.architecture.scaffolding/Configuration/CommandQueryLoggingType.cs) for the configuration class

The default values in the above class show what will be initially setup.  These can be overridden in a variety of ways

```
builder.Services.AddHohArchitecture(x =>
{
    x.CommandLogging.CommandLoggingConnectionString = "con1";
});
```
Or using appsettings.json
```
builder.Services.Configure<HohArchitectureOptions>(builder.Configuration.GetSection("RootConfig"));
```
Or you could provide [another configuration provider](SampleApi/CustomConfigurationProvider/InMemoryTestCustomConfigurationProvider.cs). These all chain together.  Be sure to call `AddHohArchitecture` first then provide any of your override methods. [see Program.cs](SampleApi/Program.cs) for examples.

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

 `builder.Services.AddTransient<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();`

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

 `builder.Services.AddTransient<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();`

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

2) Register ServiceProviderQueryCommandLocator, which will intern use IServiceProvider

```
//TODO this should be in the AddHohArchitecture setup process.
builder.Services.AddTransient<IQueryCommandLocator, ServiceProviderQueryCommandLocator>();
builder.Services.AddTransient<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();
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

3) Implement your own IQueryCommandLocator and inject as above. This can allow abstraction on resolving handlers, as well as using a different DI framework.