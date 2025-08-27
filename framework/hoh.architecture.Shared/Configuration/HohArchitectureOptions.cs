namespace hoh.architecture.Shared.Configuration
{
    public class HohArchitectureOptions
    {
        public QueryLoggingOptions QueryLogging { get; set; }
        public CommandLoggingOptions CommandLogging { get; set; }

        public bool? UseServiceCollection { get; set; } = true;

        public static HohArchitectureOptions Default => new HohArchitectureOptions
        {
            UseServiceCollection = true,
            CommandLogging = new CommandLoggingOptions
            {
                TableName = "CommandsExecuted",
                CommandLoggingConnectionString = "myConnectionString",
                EnableSensitiveDataLogging = false,
                Type = CommandQueryLoggingType.BuiltInEfProvider
            },
            QueryLogging = new QueryLoggingOptions
            {
                TableName = "QueriesExecuted",
                QueryLoggingConnectionString = "myConnectionString",
                EnableSensitiveDataLogging = false,
                Type = CommandQueryLoggingType.None
            },
        };
    }
}
