using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Command
{
    public interface ICommandHandler<in TC, TR> where TC : ICommand where TR : ICommandResult
    {
        Task<TR> ExecuteAsync(TC command);
    }
}
