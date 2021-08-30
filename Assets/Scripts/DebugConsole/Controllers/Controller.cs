using DebugConsole.UseCases;
using EventHandler.Interfaces;

namespace DebugConsole.Controllers
{
    public abstract class Controller
    {
        protected IUseCaseInputBoundary InputBoundary;
        protected readonly IEventBus EventBus;

        protected Controller(IEventBus eventBus)
        {
            EventBus = eventBus;
        }

        public abstract void Enable();
        public abstract void Disable();
    }
}