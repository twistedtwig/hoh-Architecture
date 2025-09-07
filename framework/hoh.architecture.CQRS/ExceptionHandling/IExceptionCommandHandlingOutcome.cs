using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.ExceptionHandling;

public interface IExceptionCommandHandlingOutcome
{
    public bool AllowExceptionToBubbleUp { get; set; }

    public ICommandResult? ResultOverride { get; set; }
}