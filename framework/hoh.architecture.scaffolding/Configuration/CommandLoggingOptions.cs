namespace hoh.architecture.scaffolding.Configuration
{
    public class CommandLoggingOptions
    {
        public bool UseInBuiltEfLogger { get; set; }
        public bool UseCustomerLogger { get; set; }
        public string CommandLoggingConnectionString { get; set; }
        public string TableName { get; set; }
    }
}
