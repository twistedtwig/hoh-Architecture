namespace hoh.architecture.scaffolding.Configuration
{
    public class CommandLoggingOptions
    {
        public CommandQueryLoggingType? Type { get; set; }
        public string CommandLoggingConnectionString { get; set; }
        public string TableName { get; set; }
    }
}
