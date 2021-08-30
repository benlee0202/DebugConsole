using DebugConsole.Interfaces;

namespace DebugConsole.Commands
{
    public class NotFoundDebugCommand : IDebugCommand
    {
        public string CommandToken => "CommandNotFound";
        public string CommandLog => "Command not found";

        public void Execute(string[] args)
        {
        }
    }
}