using System.Linq;
using DebugConsole.Controllers;
using DebugConsole.Entities;
using DebugConsole.Interfaces;
using DebugConsole.Presenters;
using DebugConsole.Repositories;
using DebugConsole.Utilities;
using EventHandler;
using EventHandler.Events;
using UIComponents;
using UnityEngine;

namespace DebugConsole
{
    public class Initializer : MonoBehaviour
    {
        private DebugConsolePresenter _presenter;
        private DebugConsoleController _controller;

        private void Awake()
        {
            var eventBus = new EventBus();
            var viewMap = new DisplayRepository();
            var commandGeneratorRepository = new CommandGeneratorRepository();
            _presenter = new DebugConsolePresenter(viewMap);
            _controller = new DebugConsoleController(eventBus,
                new UseCaseFactory<CommandGenerator>(commandGeneratorRepository, _presenter),
                new CommandFactory(eventBus));
            _controller.Enable();
            
            var displays = FindObjectsOfType<MonoBehaviour>().OfType<IDisplay>().ToArray();
            foreach (var display in displays)
            {
                eventBus.Publish(new OnCreateDebugConsole(display));    
            }
            
            var eventBusUsers = FindObjectsOfType<MonoBehaviour>().OfType<IEventBusUser>().ToArray();
            foreach (var eventBusUser in eventBusUsers)
            {
                eventBusUser.SetEventBus(eventBus);
            }
        }
    }
}
