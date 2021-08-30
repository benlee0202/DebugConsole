using EventHandler.Interfaces;

namespace EventHandler.Events
{
    public readonly struct OnInputCommand : IEventBase
    {
        public OnInputCommand(string commandStatement)
        {
            CommandStatement = commandStatement;
        }
        
        public string CommandStatement { get; }
    }
}