namespace SampleApi.CustomConfigurationProvider
{
    public class InMemoryTestCustomConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            Data["RootConfig:CommandLogging:CommandLoggingConnectionString"] = "override me";
        }
    }
}
