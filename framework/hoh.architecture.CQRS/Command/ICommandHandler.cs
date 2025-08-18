using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Command
{
    public interface ICommandHandler<in TC> where TC : ICommand
    {
        Task<ICommandResult> ExecuteAsync(TC command);
    }
}
