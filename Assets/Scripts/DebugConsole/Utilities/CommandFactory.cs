using System;
using System.Collections.Generic;
using DebugConsole.Commands;
using DebugConsole.Interfaces;
using EventHandler.Interfaces;

namespace DebugConsole.Utilities
{
    public class CommandFactory : IFactory<string, IDebugCommand>
    {
        private readonly Dictionary<Type, IDebugCommand> _commandMap =
            new Dictionary<Type, IDebugCommand>();
        private const string CommandSuffix = "DebugCommand";
        private const string CommandNameSpace = "DebugConsole.Commands.";

        private readonly IEventBus _eventBus;

        public CommandFactory(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IDebugCommand GetProduct(string request)
        {
            var completeRequest = $"{CommandNameSpace}{request}{CommandSuffix}";
            var type = Type.GetType(completeRequest);
            
            if (type == null) return new NotFoundDebugCommand();
            
            if (_commandMap.TryGetValue(type, out var command)) return command;
            if (!(Activator.CreateInstance(type, _eventBus) is IDebugCommand newCommand)) throw new ArgumentException();

            _commandMap.Add(type, newCommand);
            return newCommand;
        }
    }
}