namespace HoH.Architecture.Shared.Configuration
{
    public class HohArchitectureOptions
    {
        public string CommandQueryLoggingConnectionString { get; set; }

        public bool EnableSensitiveDataLogging { get; set; }

        public bool? UseServiceCollection { get; set; } = true;

        public static HohArchitectureOptions Default => new HohArchitectureOptions
        {
            UseServiceCollection = true,
            CommandQueryLoggingConnectionString = "connection string needed",
            EnableSensitiveDataLogging = false,
        };
    }
}
