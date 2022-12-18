using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Query
{
    public interface IQuery
    {
    }

    public interface IQuery<T> : IQuery
    {
    }
}
