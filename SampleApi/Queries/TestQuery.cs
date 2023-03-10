using hoh.architecture.CQRS.Query;

namespace SampleApi.Queries
{
    public class TestQuery : IQuery<string>
    {
        public string Message { get; set; }

        public TestQuery(string message)
        {
            Message = message;
        }
    }
}
