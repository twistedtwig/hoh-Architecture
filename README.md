# hoh-Architecture

This project is designed to provide a collection of nuget packages to streamline project development using CQRS, EF Core and some DDD principles.

The heart of the workflow is [CQRS](https://martinfowler.com/bliki/CQRS.html). Each action being its own command, the logic is in one place, it is seperate from other logic, so you do not end up with large service classes.

All commands and queries can be automatically logged for future investigations.

## Steps to setup an application
1. add nuget package HOH.Architecture to the application
2. In the setup add
	a. `builder.Services.AddHohArchitecture();`
	b. `app.UseHohArchitecture();`
	c. Setup any configuration overrides, such as getting from config file (this must happen after `UseHohArchitecture`)
3. register the query and command handlers with the IServiceCollection

## Configuration
There are various options that can be set at the top level via configuration, [framework\hoh.architecture.scaffolding\Configuration\CommandQueryLoggingType.cs](see CommandQueryLoggingType) for the configuration class

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
Or you could provide (SampleApi\CustomConfigurationProvider\InMemoryTestCustomConfigurationProvider.cs)[another configuration provider]. These all chain together.  Be sure to call `AddHohArchitecture` first then provide any of your override methods. [SampleApi\Program.cs](see Program.cs) for examples.

C:\Work\Hoh-Architecture\framework\hoh.architecture.scaffolding\Configuration\HohArchitectureOptions.cs
framework\hoh.architecture.scaffolding\Configuration\CommandQueryLoggingType.cs

See SampleAPI => QueryController for an example of executing a query.  It follows this convention
1. use IServiceProvider to get an instance of the IQueryExecutor.
2. Create an instance of the desired query you wish to run
3. call ExecuteAsync on your IQueryExecutor instance.
4. the handler for that query will be called, perform your business logic here.
5. handle result.
