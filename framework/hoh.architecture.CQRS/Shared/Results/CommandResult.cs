namespace HoH.Architecture.CQRS.Shared.Results
{
    public class CommandResult : ICommandResult
    {
        public bool Success { get; }
        public IReadOnlyList<IMessage> Messages { get; }
    }

    public class CommandResultWithIntId : CommandResult, ICommandResultWithIntId
    {
        public int? Id { get; set; }
    }

    public class CommandResultWithLongId : CommandResult, ICommandResultWithLongId
    {
        public long? Id { get; set; }
    }
}
