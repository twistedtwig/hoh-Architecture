namespace hoh.architecture.Shared.Configuration
{
    public class QueryLoggingOptions
    {
        public CommandQueryLoggingType? Type { get; set; }
        public string QueryLoggingConnectionString { get; set; }
        public bool EnableSensitiveDataLogging { get; set; }
        public string TableName { get; set; }
    }
}
