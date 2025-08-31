namespace hoh.architecture.Shared.Configuration
{
    public class HohArchitectureOptions
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }

        public bool EnableSensitiveDataLogging { get; set; }

        public bool? UseServiceCollection { get; set; } = true;

        public static HohArchitectureOptions Default => new HohArchitectureOptions
        {
            UseServiceCollection = true,
            ConnectionString = "connection string needed",
            TableName = "CommandQueryExecutionLogs",
            EnableSensitiveDataLogging = false,
        };
    }
}
