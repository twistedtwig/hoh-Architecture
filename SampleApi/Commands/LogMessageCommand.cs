using HoH.Architecture.CQRS.Command;

namespace SampleApi.Commands
{
    public class LogMessageCommand : ICommand
    {
        public LogMessageCommand(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
