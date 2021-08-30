using System;
using System.Collections.Generic;
using DebugConsole.Interfaces;
using DebugConsole.Presenters.PresenterInputs;
using DebugConsole.Repositories;

namespace DebugConsole.Presenters
{
    public class DebugConsolePresenter : Presenter
    {
        private readonly IRepository<IDisplay> _viewMap;
        private readonly Dictionary<Type, Action<IPresenterInput>> _actionMap;

        public DebugConsolePresenter(IRepository<IDisplay> viewMap)
        {
            _viewMap = viewMap;
            _actionMap = new Dictionary<Type, Action<IPresenterInput>>
            {
                {typeof(CreateDebugConsolePresenterInput), 
                    input => CreateDebugConsole((CreateDebugConsolePresenterInput) input)},
                {typeof(ExecuteCommandPresenterInput), input =>
                {
                    ExecuteCommand((ExecuteCommandPresenterInput) input);
                    DisplayInformation((ExecuteCommandPresenterInput) input);
                }},
            };
        }

 

        public override void Execute<TInput>(TInput input)
        {
            if (_actionMap.TryGetValue(input.GetType(), out var action)) action.Invoke(input);
        }
        
        private void CreateDebugConsole(CreateDebugConsolePresenterInput input)
        {
            _viewMap.Add(input.Display);
        }
        
        private void ExecuteCommand(ExecuteCommandPresenterInput input)
        {
            input.DebugCommand.Execute(input.Arguments);
        }
        
        private void DisplayInformation(ExecuteCommandPresenterInput input)
        {
            var debugConsoleDisplay = _viewMap.GetItem(input.ViewID);
            debugConsoleDisplay.Display(input.DebugCommand);
        }
    }
}