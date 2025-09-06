using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.ExceptionHandling
{
    public interface IExceptionQueryHandlingOutcome<T> where T : class
    {
        public bool AllowExceptionToBubbleUp { get; set; }

        public IQueryResult<T>? ResultOverride { get; set; }
    }
}
