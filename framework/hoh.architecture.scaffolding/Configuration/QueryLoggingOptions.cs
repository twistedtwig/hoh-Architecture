namespace hoh.architecture.scaffolding.Configuration
{
    public class QueryLoggingOptions
    {
        public CommandQueryLoggingType? Type { get; set; }
        public string QueryLoggingConnectionString { get; set; }
        public string TableName { get; set; }
    }
}
