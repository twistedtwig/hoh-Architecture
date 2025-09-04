using HoH.Architecture.CQRS.Command;

namespace SampleApi.Commands
{
    public class LogMessageCommand : ICommand
    {
        public string Message { get; set; }
    }
}
