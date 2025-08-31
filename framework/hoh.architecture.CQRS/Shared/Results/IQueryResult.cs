namespace HoH.Architecture.CQRS.Shared.Results
{
    public interface IQueryResult<out T> : IResult where T : class
    {
        public T Result { get; }
    }
}
