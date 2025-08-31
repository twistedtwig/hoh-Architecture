cd HoH.SetupWebApi

dotnet ef migrations add %1 --project ../HoH.Architecture.CQRS/HoH.Architecture.CQRS.csproj --context LoggingDbContext

 cd ..