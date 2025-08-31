using System.Collections.ObjectModel;

namespace HoH.Architecture.CQRS.Shared.Results
{
    public class QueryResult<T> : IQueryResult<T> where T : class
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
