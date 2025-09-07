using HoH.Architecture.CQRS.Shared.QueryCommandHandling;
using HoH.Architecture.CQRS.Shared.Results;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Commands;

namespace SampleApi.Controllers;

[ApiController]
[Route("Commands")]
public class CommandController : ControllerBase
{
    private readonly IQueryCommandExecutor _executor;

    public CommandController(IQueryCommandExecutor executor)
    {
        _executor = executor;
    }

    [HttpPost]
    [Route("message")]
    public async Task<ICommandResult> AddMessage(string message)
    {
        var command = new LogMessageCommand(message);
        var result = await _executor.ExecuteCommandAsync<LogMessageCommand>(command);
        return result;
    }
}