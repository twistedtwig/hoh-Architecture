namespace hoh.architecture.Shared.Configuration
{
    public class CommandLoggingOptions
    {
        public CommandQueryLoggingType? Type { get; set; }
        public string CommandLoggingConnectionString { get; set; }
        public bool EnableSensitiveDataLogging { get; set; }
        public string TableName { get; set; }
    }
}
