namespace HoH.Architecture.CQRS.Logging
{
    public class LoggingEntity
    {
        public long Id { get; set; }

        public QueryCommandLoggingType Type { get; set; }

        public string HandlerType { get; set; }
        public string ItemType { get; set; }
        public DateTime ExecutionTime { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public bool Success { get; set; }

        public string ItemJson { get; set; }

        public string Error { get; set; }
    }
}
