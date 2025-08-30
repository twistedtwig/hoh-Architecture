namespace hoh.architecture.Shared.Configuration
{
    public class HohArchitectureOptions
    {
        public QueryLoggingOptions QueryLogging { get; set; }
        public CommandLoggingOptions CommandLogging { get; set; }

        public string ConnectionString { get; set; }
        public bool EnableSensitiveDataLogging { get; set; }

        public bool? UseServiceCollection { get; set; } = true;

        public static HohArchitectureOptions Default => new HohArchitectureOptions
        {
            UseServiceCollection = true,
            ConnectionString = "myConnectionString",
            EnableSensitiveDataLogging = false,
            CommandLogging = new CommandLoggingOptions
            {
                TableName = "CommandsExecuted",
                
                Type = CommandQueryLoggingType.BuiltInEfProvider
            },
            QueryLogging = new QueryLoggingOptions
            {
                TableName = "QueriesExecuted",
                Type = CommandQueryLoggingType.None
            },
        };
    }
}
