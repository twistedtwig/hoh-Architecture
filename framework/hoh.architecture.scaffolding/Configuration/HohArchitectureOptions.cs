namespace hoh.architecture.scaffolding.Configuration
{
    public class HohArchitectureOptions
    {
        public QueryLoggingOptions QueryLogging { get; set; }
        public CommandLoggingOptions CommandLogging { get; set; }

        public static HohArchitectureOptions Default => new HohArchitectureOptions
        {
            CommandLogging = new CommandLoggingOptions
            {
                TableName = "CommandsExecuted",
                CommandLoggingConnectionString = "myConnectionString",
                Type = CommandQueryLoggingType.BuiltInEfProvider
            },
            QueryLogging = new QueryLoggingOptions
            {
                TableName = "QueriesExecuted",
                QueryLoggingConnectionString = "myConnectionString",
                Type = CommandQueryLoggingType.None
            },
        };
    }
}
