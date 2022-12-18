using System.Collections.ObjectModel;

namespace hoh.architecture.CQRS.Shared.Results;

public class ExceptionalMessage : IExceptionalMessage
{
    public IReadOnlyList<string> Errors { get; }
    public string StackTrace { get; }

    public ExceptionalMessage(params string[] errors)
    {
        Errors = new ReadOnlyCollection<string>(errors);
        StackTrace = string.Empty;
    }

    public ExceptionalMessage(Exception ex, params string[] errors) :this(errors)
    {
        StackTrace = ex?.StackTrace ?? string.Empty;
    }
}