using EventHandler.Interfaces;

namespace DebugConsole.Commands
{
    internal class TestArgumentDebugCommand : DebugCommandBase
    {
        public override string CommandToken => "TestArgument";
        public override string CommandLog { get; protected set; }

        public TestArgumentDebugCommand(IEventBus eventBus) : base(eventBus)
        {
        }

        public override void Execute(string[] args)
        {
            CommandLog = $"{args[0].GetType()}: {args[0]}, ";
            
            if (int.TryParse(args[1], out var number))
            {
                CommandLog += $"{number.GetType()}: {number}, ";
            }
            else
            {
                CommandLog = "Cannot convert the argument to int";
                return;
            }

            if (float.TryParse(args[2], out var numberWithPoint))
            {
                CommandLog += $"{numberWithPoint.GetType()}: {numberWithPoint}, ";
            }
            else
            {
                CommandLog = "Cannot convert the argument to float";
                return;
            }
            
            if (bool.TryParse(args[3], out var boolean))
            {
                CommandLog += $"{boolean.GetType()}: {boolean}";
            }
            else
            {
                CommandLog = "Cannot convert the argument to bool";
            }
        }
    }
}
