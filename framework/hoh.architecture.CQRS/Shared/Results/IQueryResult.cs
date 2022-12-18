namespace hoh.architecture.CQRS.Shared.Results
{
    public interface IQueryResult<out T> : IResult
    {
        public T Result { get; }
    }
}
