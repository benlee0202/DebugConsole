using System;
using System.Linq;
using DebugConsole.Interfaces;
using DebugConsole.UseCases;
using DebugConsole.UseCases.UseCaseInputs;
using EventHandler.Events;
using EventHandler.Interfaces;

namespace DebugConsole.Controllers
{
    public class DebugConsoleController : Controller
    {
        private readonly IFactory<Type, IUseCaseInputBoundary> _useCaseFactory;
        private readonly IFactory<string, IDebugCommand> _commandFactory;
        private int _currentDebugConsoleID;
        public DebugConsoleController(IEventBus eventBus,
            IFactory<Type, IUseCaseInputBoundary> useCaseFactory, IFactory<string, IDebugCommand> commandFactory) : base(eventBus)
        {
            _useCaseFactory = useCaseFactory;
            _commandFactory = commandFactory;
        }

        public override void Enable()
        {
            EventBus.Subscribe<OnCreateDebugConsole>(CallCreateDebugConsole);
            EventBus.Subscribe<OnInputCommand>(CallGetCommandUseCase);
        }

        public override void Disable()
        {
            EventBus.Unsubscribe<OnCreateDebugConsole>(CallCreateDebugConsole);
            EventBus.Unsubscribe<OnInputCommand>(CallGetCommandUseCase);
        }

        private void CallCreateDebugConsole(OnCreateDebugConsole gameEvent)
        {
            _currentDebugConsoleID = gameEvent.Display.ViewID;
            InputBoundary = _useCaseFactory.GetProduct(typeof(CreateCommandGeneratorEntityUseCase));
            InputBoundary.Execute(
                new CreateCommandGeneratorUseCaseInput(gameEvent.Display.ViewID, _commandFactory, gameEvent.Display));
        }

        private void CallGetCommandUseCase(OnInputCommand gameEvent)
        {
            var splits = gameEvent.CommandStatement.Split(' ');
            var token = splits[0];
            var arguments = splits.Skip(1).ToArray();

            InputBoundary = _useCaseFactory.GetProduct(typeof(ExecuteCommandUseCase));
            InputBoundary.Execute(new ExecuteCommandUseCaseInput(_currentDebugConsoleID, token, arguments));
        }
    }
}