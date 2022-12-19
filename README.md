# hoh-Architecture

This project is designed to provide a collection of nuget packages to streamline project development using CQRS, EF Core and some DDD principles.

The heart of the workflow is [CQRS](https://martinfowler.com/bliki/CQRS.html). Each action being its own command, the logic is in one place, it is seperate from other logic, so you do not end up with large service classes.

All commands and queries can be automatically logged for future investigations.

## Steps to setup an application
1) add nuget package HOH.Architecture to the application
2) In the setup add
	a) `builder.Services.AddHohArchitecture();`
	b) `app.UseHohArchitecture();`
3) register the query and command handlers with the IServiceCollection

See SampleAPI => QueryController for an example of executing a query.  It follows this convention
1) use IServiceProvider to get an instance of the IQueryExecutor.
2) Create an instance of the desired query you wish to run
3) call ExecuteAsync on your IQueryExecutor instance.
4) the handler for that query will be called, perform your business logic here.
5) handle result.
