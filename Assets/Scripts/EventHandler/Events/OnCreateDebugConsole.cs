using DebugConsole.Interfaces;
using EventHandler.Interfaces;

namespace EventHandler.Events
{
    public readonly struct OnCreateDebugConsole : IEventBase
    {
        public OnCreateDebugConsole(IDisplay display)
        {
            Display = display;
        }

        public IDisplay Display { get; }
    }
}