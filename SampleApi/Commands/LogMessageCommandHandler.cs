using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Shared.Results;

namespace SampleApi.Commands
{
    public class LogMessageCommandHandler : ICommandHandler<LogMessageCommand>
    {
        private readonly ExampleDbContext _exampleDbContext;
        public LogMessageCommandHandler(ExampleDbContext exampleDbContext)
        {
            _exampleDbContext = exampleDbContext;
        }

        public async Task<ICommandResult> ExecuteAsync(LogMessageCommand command)
        {
            await _exampleDbContext.Set<Message>().AddAsync(new Message {Text = command.Message, When = DateTime.Now});
            await _exampleDbContext.SaveChangesAsync();

            return new CommandResult(true);
        }
    }
}
