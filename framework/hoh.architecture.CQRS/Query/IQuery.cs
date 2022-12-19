namespace hoh.architecture.CQRS.Query
{
    public interface IBaseQuery
    {
    }

    public interface IQuery<out T> : IBaseQuery
    {
    }
}
