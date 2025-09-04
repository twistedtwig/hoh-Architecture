using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.Command
{
    public interface ICommandHandler<in TC, TR> where TC : ICommand where TR : ICommandResult
    {
        Task<TR> ExecuteAsync(TC command);
    }
}
