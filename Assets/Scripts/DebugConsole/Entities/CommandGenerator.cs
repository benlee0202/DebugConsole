using System;
using DebugConsole.Interfaces;

namespace DebugConsole.Entities
{
    public class CommandGenerator : Entity
    {
        private readonly IFactory<string, IDebugCommand> _commandFactory;
        
        public CommandGenerator(int entityID, IFactory<string, IDebugCommand> commandFactory) : base(entityID)
        {
            _commandFactory = commandFactory;
        }

        public IDebugCommand GetCommand(string commandToken)
        {
            return _commandFactory.GetProduct(commandToken);
        }
    }
}