using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;
using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.ExceptionHandling
{
    public interface IExceptionHandler
    {
        Task<IExceptionQueryHandlingOutcome<TR>?> HandleQueryExecutionExceptionAsync<TC, TR>(Exception ex, TC query) where TC : IQuery where TR : class;

        /// <summary>
        /// Called when command handler throws an exception. Allows:
        /// - Log / process the exception then allow the code to continue as before
        /// - Swallow the exception and return the override provided.
        ///
        /// If the exception should not bubble, the return type dto should be provided.
        /// </summary>
        /// <typeparam name="TC"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="ex"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<IExceptionCommandHandlingOutcome<TR>?> HandleCommandExecutionExceptionAsync<TC, TR>(Exception ex, TC command) where TC : ICommand where TR : ICommandResult;
    }
}
