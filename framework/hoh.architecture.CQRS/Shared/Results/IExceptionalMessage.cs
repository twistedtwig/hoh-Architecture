namespace hoh.architecture.CQRS.Shared.Results
{
    public interface IExceptionalMessage : IMessage
    {
        public IReadOnlyList<string> Errors { get; }

        public string StackTrace { get; }
    }
}
