namespace hoh.architecture.scaffolding.Configuration
{
    public class QueryLoggingOptions
    {
        public bool UseInBuiltEfLogger { get; set; }
        public bool UseCustomerLogger { get; set; }
        public string QueryLoggingConnectionString { get; set; }
        public string TableName { get; set; }
    }
}
