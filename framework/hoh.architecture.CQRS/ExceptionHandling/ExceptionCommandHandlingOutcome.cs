using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.ExceptionHandling;

public class ExceptionCommandHandlingOutcome<T> : IExceptionCommandHandlingOutcome<T> where T : ICommandResult
{
    public bool AllowExceptionToBubbleUp { get; set; }

    public T ResultOverride { get; set; }
}