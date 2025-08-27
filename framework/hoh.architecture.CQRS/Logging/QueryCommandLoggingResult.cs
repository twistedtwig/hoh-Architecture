namespace hoh.architecture.CQRS.Logging
{
    public class QueryCommandLoggingResult
    {
        public DateTime ExecutionTime { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public bool Success { get; set; }

        public string Error { get; set; }
    }
}
