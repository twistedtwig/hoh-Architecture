using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.Command
{
    public interface ICommandHandler<in TC> where TC : ICommand
    {
        Task<ICommandResult> ExecuteAsync(TC command);
    }
}
