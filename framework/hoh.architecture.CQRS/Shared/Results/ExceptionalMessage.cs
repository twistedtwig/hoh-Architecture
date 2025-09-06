using System.Collections.ObjectModel;
using System.Text;
using Microsoft.IdentityModel.Tokens;

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

        if (errors.IsNullOrEmpty())
        {
            Errors = new ReadOnlyCollection<string>(new List<string> { ex.Message });
        }
    }

    public string? Text
    {
        get
        {
            var builder = new StringBuilder();
            foreach (var error in Errors)
            {
                if (!string.IsNullOrWhiteSpace(error))
                {
                    builder.AppendLine(error);
                }
            }

            if (!string.IsNullOrWhiteSpace(StackTrace))
            {
                builder.AppendLine(StackTrace);
            }

            return builder.ToString();
        }
    }
}