namespace hoh.architecture.CQRS.Shared.Results
{
    public interface ICommandResult : IResult
    {
    }

    public interface ICommandResultWithIntId : ICommandResult
    {
        public int? Id { get; protected set; }
    }

    public interface ICommandResultWithLongId : ICommandResult
    {
        public long? Id { get; protected set; }
    }
}