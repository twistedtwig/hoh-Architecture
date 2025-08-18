namespace hoh.architecture.CQRS.Command
{
    public interface ICommand
    {
    }

    public interface ICommand<out TC, out TR> : ICommand
    {
    }
}
