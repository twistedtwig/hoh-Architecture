using System.Collections.ObjectModel;

namespace hoh.architecture.CQRS.Shared.Results
{
    public class QueryResult<T> : IQueryResult<T>
    {
        public bool Success { get; }
        public IReadOnlyList<IMessage> Messages { get; }
        public T Result { get; }

        public QueryResult(bool success, T result, params IMessage[] messages)
        {
            Success = success;
            Result = result;
            Messages = new ReadOnlyCollection<IMessage>(messages);
        }
    }
}
