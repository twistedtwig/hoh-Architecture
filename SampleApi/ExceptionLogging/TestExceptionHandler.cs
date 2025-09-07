using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.ExceptionHandling;
using HoH.Architecture.CQRS.Query;

namespace SampleApi.ExceptionLogging
{
    public class TestExceptionHandler : IExceptionHandler
    {
        public Task<IExceptionQueryHandlingOutcome<TR>?> HandleQueryExecutionExceptionAsync<TC, TR>(Exception ex, TC query) where TC : IQuery where TR : class
        {
            var dto = new ExceptionQueryHandlingOutcome<TR> { AllowExceptionToBubbleUp = false };
            // var dto = new ExceptionQueryHandlingOutcome<TR> {AllowExceptionToBubbleUp = false, ResultOverride = new QueryResult<TR>(false, null, new ExceptionalMessage("it all went wrong"))};
            return Task.FromResult((IExceptionQueryHandlingOutcome<TR>?)dto);
        }

        public Task<IExceptionCommandHandlingOutcome?> HandleCommandExecutionExceptionAsync<TC>(Exception ex, TC command) where TC : ICommand
        {
            // if (typeof(TR) == typeof(LogMessageCommand))
            // {
            //     var dto = new ExceptionCommandHandlingOutcome<TR> { AllowExceptionToBubbleUp = false, ResultOverride = new (TR)CommandResult(false) };
            // }
            //
            // return new Task<>()

            throw new NotImplementedException();
        }
    }
}
