namespace hoh.architecture.CQRS.Shared.Results
{
    public interface IResult
    {
        public bool Success { get; }
        public IReadOnlyList<IMessage> Messages { get; }
    }
}
