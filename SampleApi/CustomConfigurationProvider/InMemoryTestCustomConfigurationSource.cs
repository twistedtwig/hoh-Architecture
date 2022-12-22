namespace SampleApi.CustomConfigurationProvider
{
    public class InMemoryTestCustomConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new InMemoryTestCustomConfigurationProvider();
        }
    }
}
