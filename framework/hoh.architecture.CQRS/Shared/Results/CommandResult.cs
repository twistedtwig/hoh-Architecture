namespace HoH.Architecture.CQRS.Shared.Results
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, IReadOnlyList<IMessage> messages)
        {
            Success = success;
            Messages = messages;
        }
        public bool Success { get; }
        public IReadOnlyList<IMessage> Messages { get; }
    }

    public class CommandResultWithIntId : CommandResult, ICommandResultWithIntId
    {
        public CommandResultWithIntId(bool success, IReadOnlyList<IMessage> messages) : base(success, messages)
        {
        }

        public CommandResultWithIntId(bool success, int id, IReadOnlyList<IMessage> messages) : base(success, messages)
        {
            Id = id;
        }

        public int? Id { get; set; }
    }

    public class CommandResultWithLongId : CommandResult, ICommandResultWithLongId
    {
        public CommandResultWithLongId(bool success, IReadOnlyList<IMessage> messages) : base(success, messages)
        {
        }

        public CommandResultWithLongId(bool success, long id, IReadOnlyList<IMessage> messages) : base(success, messages)
        {
            Id = id;
        }

        public long? Id { get; set; }
    }
}
