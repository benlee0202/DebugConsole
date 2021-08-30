using EventHandler.Events;
using EventHandler.Interfaces;

namespace DebugConsole.Commands
{
    public class TestDebugCommand : DebugCommandBase
    {
        public override string CommandToken => "Test";
        public sealed override string CommandLog { get; protected set; }

        public TestDebugCommand(IEventBus eventBus) : base(eventBus)
        {
            CommandLog = "For unit test";
        }

        public override void Execute(string[] args)
        {
            EventBus.Publish(new OnTestCommandExecute());
        }
    }
}
