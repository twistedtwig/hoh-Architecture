using System.Collections.ObjectModel;

namespace HoH.Architecture.CQRS.Shared.Results
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success) : this(success, Array.Empty<IMessage>()) { }

        public CommandResult(bool success, params IMessage[] messages)
        {
            Success = success;
            Messages = new ReadOnlyCollection<IMessage>(messages);
        }
        public bool Success { get; }
        public IReadOnlyList<IMessage> Messages { get; }
    }

    public class CommandResultWithIntId : CommandResult, ICommandResultWithIntId
    {
        public CommandResultWithIntId(bool success) : this(success, Array.Empty<IMessage>()) { }

        public CommandResultWithIntId(bool success, params IMessage[] messages) : base(success, messages)
        {
        }

        public CommandResultWithIntId(bool success, int id, params IMessage[] messages) : base(success, messages.ToArray())
        {
            Id = id;
        }

        public int? Id { get; set; }
    }

    public class CommandResultWithLongId : CommandResult, ICommandResultWithLongId
    {
        public CommandResultWithLongId(bool success) : this(success, Array.Empty<IMessage>()) { }

        public CommandResultWithLongId(bool success, params IMessage[] messages) : base(success, messages)
        {
        }

        public CommandResultWithLongId(bool success, long id, params IMessage[] messages) : base(success, messages)
        {
            Id = id;
        }

        public long? Id { get; set; }
    }
}
