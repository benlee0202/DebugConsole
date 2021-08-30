using DebugConsole.Interfaces;
using EventHandler.Interfaces;

namespace DebugConsole.Commands
{
    public abstract class DebugCommandBase : IDebugCommand
    {
        protected readonly IEventBus EventBus;

        protected DebugCommandBase(IEventBus eventBus)
        {
            EventBus = eventBus;
        }

        public abstract string CommandToken { get; }
        public abstract string CommandLog { get; protected set; }
        public abstract void Execute(string[] args);
    }
}
