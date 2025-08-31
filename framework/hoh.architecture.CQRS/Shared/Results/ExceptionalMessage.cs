using System.Collections.ObjectModel;
using System.Text;

namespace HoH.Architecture.CQRS.Shared.Results;

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
        //TODO handle ex.message
    }

    public string? Text
    {
        get
        {
            //TODO handle nulls
            var builder = new StringBuilder();
            foreach (var error in Errors)
            {
                builder.AppendLine(error);
            }

            builder.AppendLine(StackTrace);

            return builder.ToString();
        }
    }
}